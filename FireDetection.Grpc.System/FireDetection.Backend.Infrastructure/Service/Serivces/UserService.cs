using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
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
        public Task<bool> ActiveUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInformationResponse> CreateUser(CreateUserRequest request)
        {
          User user = _mapper.Map<User>(request);
            user.RoleId = request.UserRole;
            user.Status = "None";
          /*  User userT = new User()
            {
                RoleId = 1,
                Name = "comsuonhocmon",
                SecurityCode = "2lv",
                Phone = "030303",
                Password = "23"
                ,
                Email = "comsuonhocmon@gmail.com",
                Status = "Check"
            };*/
            _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveChangeAsync();

            IQueryable<User> data =  await _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<UserInformationResponse>(data.FirstOrDefault(x => x.Email  == request.Email));
        }

        public Task<bool> InactiveUser(Guid userId)
        {
            throw new NotImplementedException();
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

        public Task<UserInformationResponse> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
