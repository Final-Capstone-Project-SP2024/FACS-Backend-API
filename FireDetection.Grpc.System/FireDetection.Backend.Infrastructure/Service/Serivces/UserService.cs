using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Helpers.GetHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FireDetection.Backend.Domain.Ultilities;
using AutoMapper.QueryableExtensions;
using FireDetection.Backend.Domain.DTOs.State;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = config;
        }

        public async Task<bool> ActiveUser(Guid userId)
        {
            if(!await CheckUserStatus(userId, UserState.Active)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already actived in system");
            User user = await GetUserById(userId);
            user.LastModified = DateTime.UtcNow;
            user.Status = UserState.Active;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public async Task<UserInformationResponse> CreateUser(CreateUserRequest request)
        {

            if (!await CheckDuplicateEmail(request.Email)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this email in system");


            if (!await CheckDuplicatePhone(request.Phone)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this phone number   in system");

            User user = _mapper.Map<User>(request);
            user.SecurityCode = await GenerateSecurityCode();
            _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();

            IQueryable<User> data = await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<UserInformationResponse>(data.FirstOrDefault(x => x.Email == request.Email));
        }

        public async Task<bool> InactiveUser(Guid userId)
        {
            if (!await CheckUserStatus(userId, "banned")) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already banned in system");
            User user = await GetUserById(userId);
            user.LastModified = DateTime.UtcNow;
            user.Status = "banned";

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest req)
        {
            var user = _unitOfWork.UserRepository.Include(u => u.Role).Where(u => u.SecurityCode == req.SecurityCode && u.Password == req.Password).FirstOrDefault();
            string secretKeyConfig = _configuration["JWTSecretKey:SecretKey"];
            DateTime secretKeyDatetime = DateTime.UtcNow;
            string refreshToken = GetRefreshToken();

            string accessToken = GenerateJWT(user, secretKeyConfig, secretKeyDatetime);
            return new UserLoginResponse
            {
                SecurityCode = user.SecurityCode,
                Name = user.Name,
                Phone = user.Phone,
                Role = new RoleResponse
                {
                    RoleName = user.Role.RoleName.ToString(),
                },
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        private string GetRefreshToken()
        {
            var randomCharacter = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomCharacter);
            return Convert.ToBase64String(randomCharacter);
        }

        private string GenerateJWT(User user, string secretKey, DateTime now)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                };

            var token = new JwtSecurityToken(
                issuer: secretKey,
                audience: secretKey,
                claims: claims,
                expires: now.AddMinutes(86400),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<UserInformationResponse> UpdateUser(Guid id, UpdateUserRequest req)
        {
            User user = await GetUserById(id);
            if (user == null)
            {
                throw new Exception();
            }
            _mapper.Map(user, req);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UserInformationResponse>(_unitOfWork.UserRepository.Where(x => x.Id == id).FirstOrDefault());

        }

        public async Task<User> GetUserById(Guid id)
        {
            IQueryable<User> users = await _unitOfWork.UserRepository.GetAll();

            return users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<string> GenerateSecurityCode()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            User user = users.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (user is null)
            {
                return "XXX_001";
            }
            else
            {
                if (int.TryParse(user.SecurityCode.Substring(4), out int numericPart))
                {
                    numericPart++;
                }
                return $"XXX_{numericPart:D3}";
            }
        }

        public async Task<bool> CheckDuplicatePhone(string phone)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var user = users.FirstOrDefault(x => x.Phone == phone);
            if (user is null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckDuplicateEmail(string email)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var user = users.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> CheckUserStatus(Guid id, string status)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var user = users.FirstOrDefault(x => x.Status == status && x.Id == id);
            if (user is null)
            {
                return true;
            }
            return false;
        }
            
        public async Task<PagedResult<UserInformationResponse>> GetAllUsers(PagingRequest pagingRequest, UserRequest request)
        {
            if (pagingRequest.ColName == null)
            {
                pagingRequest.ColName = "SecurityCode"; //Change Default ID
            }

            var userEntity = _mapper.Map<User>(request);
            var usersQuery = await _unitOfWork.UserRepository.GetAll();
            var filterQuery = LinqUtils.DynamicFilter<User>(usersQuery, userEntity);
            var usersProjected = filterQuery.ProjectTo<UserInformationResponse>(_mapper.ConfigurationProvider);

            #region filter
            if (request.SecurityCode != null)
            {
                usersProjected = usersProjected.Where(_ => _.SecurityCode == request.SecurityCode);
            }

            if(request.Name != null)
            {
                usersProjected = usersProjected.Where(_ => _.Name == request.Name);
            }

            if (request.Email != null)
            {
                usersProjected = usersProjected.Where(_ => _.Email == request.Email);
            }

            if (request.Phone != null)
            {
                usersProjected = usersProjected.Where(_ => _.Phone == request.Phone);
            }

            if (request.Status != null)
            {
                usersProjected = usersProjected.Where(_ => _.Status == request.Status);
            }
            #endregion

            var sort = PageHelper<UserInformationResponse>.Sorting(pagingRequest.SortType, usersProjected, pagingRequest.ColName).ToList();
            var pagedUsers = PageHelper<UserInformationResponse>.Paging(sort, pagingRequest.Page, pagingRequest.PageSize);

            return pagedUsers;
        }

    }
}
