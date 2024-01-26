using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
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
            User user = await GetUserById(userId);
            user.LastModified = DateTime.UtcNow;
            user.Status = "Active";

             _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();

            return true;

        }

        public async Task<UserInformationResponse> CreateUser(CreateUserRequest request)
        {

            if (!await CheckDuplicateEmail(request.Email)) throw new Exception();

            User user = _mapper.Map<User>(request);
            user.SecurityCode =  await GenerateSecurityCode();
            user.Status = "None";
            _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();

            IQueryable<User> data =  await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<UserInformationResponse>(data.FirstOrDefault(x => x.Email  == request.Email));
        }

        public async Task<bool> InactiveUser(Guid userId)
        {

            User user = await GetUserById(userId);
            user.LastModified = DateTime.UtcNow;
            user.Status = "Banned";

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

        private async Task<User> GetUserById(Guid id)
        {
            IQueryable<User> users = await _unitOfWork.UserRepository.GetAll();

            return  users.FirstOrDefault(x => x.Id == id);
         }

        private async Task<string> GenerateSecurityCode()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            User user =  users.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if(user is null)
            {
                return "XXX_001";
            }
            else
            {
                if (int.TryParse(user.SecurityCode.Substring(4), out int numericPart))
                {
                    // Increment the numeric part
                    numericPart++;

                  
                }
                return $"XXX_{numericPart:D3}";
            }
        }

        private async Task<bool> CheckDuplicateEmail(string email)
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var user =  users.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return true;
            }
            return false;
        }
    }
}
