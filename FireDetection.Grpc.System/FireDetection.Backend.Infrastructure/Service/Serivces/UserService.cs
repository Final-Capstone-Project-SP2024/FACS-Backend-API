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
using FireDetection.Backend.Domain.Helpers.EmailHandler;
using Google.Apis.Auth.OAuth2;
using System.Security;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class UserService : IUserService
    {
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IClaimsService claimsService, IMemoryCacheService memoryCacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = config;
            _claimsService = claimsService;
            _memoryCacheService = memoryCacheService;
        }

        public async Task<bool> ActiveUser(Guid userId)
        {
            if (!await CheckUserStatus(userId, UserState.Active)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already actived in system");
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

            request.UserRole = 2;
            User user = _mapper.Map<User>(request);
            user.SecurityCode = await GenerateSecurityCode();
            _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();

            IQueryable<User> data = await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<UserInformationResponse>(data.FirstOrDefault(x => x.Email == request.Email));
        }

        public async Task<bool> InactiveUser(Guid userId)
        {
            if (!await CheckUserStatus(userId, UserState.Inactive)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already banned in system");
            User user = await GetUserById(userId);
            user.LastModified = DateTime.UtcNow;
            user.Status = UserState.Inactive;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest req)
        {
            var user = _unitOfWork.UserRepository.Include(u => u.Role).Where(u => u.SecurityCode == req.SecurityCode && u.Password == req.Password).FirstOrDefault();
            if (user is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Check your security code and password again");
            if (user.Status == UserState.Inactive)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "User have already banned in system");
            }
            string secretKeyConfig = _configuration["JWTSecretKey:SecretKey"];
            DateTime secretKeyDatetime = DateTime.UtcNow;
            string refreshToken = GetRefreshToken();

            string accessToken = GenerateJWT(user, secretKeyConfig, secretKeyDatetime);
            return new UserLoginResponse
            {
                Id = user.Id,
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
                    new Claim("UserId", user.Id.ToString()),

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
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Not find this user");
            }
            _mapper.Map(req, user);
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
            usersQuery = usersQuery.Include(x => x.Role);

            var records = await usersQuery.ToListAsync();

            var filterQuery = LinqUtils.DynamicFilter<User>(usersQuery, userEntity);
            var usersProjected = filterQuery.ProjectTo<UserInformationResponse>(_mapper.ConfigurationProvider);

            #region filter
            if (request.SecurityCode != null)
            {
                usersProjected = usersProjected.Where(_ => _.SecurityCode == request.SecurityCode);
            }

            if (request.Name != null)
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

        public async Task<bool> SendEmail(string email)
        {
            SendMailHandler sendMail = new SendMailHandler(_configuration);
            var user = await _unitOfWork.UserRepository.Where(x => x.Email == email).FirstOrDefaultAsync();

            await sendMail.SendMail("Account to access to System", email, user.SecurityCode, user.Password, user.Name);
            return true;

        }

        public async Task<UserInformationDetailResponse> GetDetail(Guid userId)
        {
            UserInformationDetailResponse userDetail = new UserInformationDetailResponse();
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var userContract = await _unitOfWork.ContractRepository.Where(x => x.UserID == userId).FirstOrDefaultAsync();
            var userTransaction = _unitOfWork.TransactionRepository.Where(x => x.UserID == userId).ToList();
            _mapper.Map(user, userDetail);
            userDetail.UserContract = _mapper.Map<ContractDetailResponse>(userContract);
            userDetail.UserTransaction = userTransaction.Select(x => _mapper.Map<TransactionGeneralResponse>(x)).ToList();

            return userDetail;
        }

        public async Task ForgotPassword(string SecurityCode)
        {
            SendMailHandler SendMail = new SendMailHandler(_configuration);
            var user = await _unitOfWork.UserRepository.Where(x => x.SecurityCode == SecurityCode).FirstOrDefaultAsync();
            int otp = await RandomNumber();
            if (user is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "User have already banned in system");

            await _memoryCacheService.SaveOTP(otp, user.Email);
            await SendMail.SendOTP(user.Email, otp);

        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _unitOfWork.UserRepository.Where(x => x.SecurityCode == request.SecurityCode).FirstOrDefaultAsync();
            int otpCheck = await _memoryCacheService.GetOTP(user.Email);
            if (request.OTPSending == otpCheck) return true;


            user.Password = request.newPassword;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();
            return false;
        }

        internal async Task<int> RandomNumber()
        {
            Random rand = new Random();
            return rand.Next(100000, 1000000);
        }

        public async Task<UserInformationDetailResponse> ChangePasswordByUser(ChangePasswordByUserRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetById(_claimsService.GetCurrentUserId);
            if (user is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "User have already banned in system");

            if (user.Password != request.OldPassword) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Your old password is wrong ");

            user.Password = request.NewPassword;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UserInformationDetailResponse>(_unitOfWork.UserRepository.GetById(user.Id));

        }
    }
}
