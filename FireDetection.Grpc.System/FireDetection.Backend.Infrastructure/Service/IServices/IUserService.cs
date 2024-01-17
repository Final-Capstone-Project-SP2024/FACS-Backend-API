﻿using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
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
        public Task<UserInformationResponse> UpdateUser();
        public Task<UserLoginResponse> Login(UserLoginRequest req);
    }
}