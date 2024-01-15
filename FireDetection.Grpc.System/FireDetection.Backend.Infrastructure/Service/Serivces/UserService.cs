using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;   
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

        public Task<UserInformationResponse> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
