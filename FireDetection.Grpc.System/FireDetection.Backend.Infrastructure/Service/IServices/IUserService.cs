using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IUserService
    {
        public Task<UserInformationResponse> CreateUser(CreateUserRequest request);
        public Task<bool> ActiveUser(Guid userId);
        public Task<bool> InactiveUser(Guid userId);
        public Task<UserLoginResponse> Login(UserLoginRequest req);
        public Task<PagedResult<UserInformationResponse>> GetAllUsers(PagingRequest pagingRequest, UserRequest request);
        public Task<UserInformationResponse> UpdateUser(UpdateUserRequest req);
        public Task<User> GetUserById(Guid id);
        public Task<bool> CheckUserStatus(Guid id, string status);

        public  Task<bool> CheckDuplicateEmail(string email);

        public  Task<bool> CheckDuplicatePhone(string phone);

        public  Task<string> GenerateSecurityCode();

        public Task<bool> SendEmail(string email);

        public Task<UserInformationDetailResponse> GetDetail(Guid userId);

        public Task ForgotPassword(string SecurityCode);

        public Task<bool> ChangePassword(ChangePasswordRequest request);

        public Task<UserInformationDetailResponse> ChangePasswordByUser(ChangePasswordByUserRequest request);

        public Task<IEnumerable<UserInformationResponse>> UnRegisterLocaiton(Guid locationId);

        Task<RefreshTokenResponse> GetAccessTokenByRefreshToken(string Refreshtoken);

        Task<UserInformationResponse> UpdateUserByManager(Guid userId, UpdateUserRequest request);
    }
}
