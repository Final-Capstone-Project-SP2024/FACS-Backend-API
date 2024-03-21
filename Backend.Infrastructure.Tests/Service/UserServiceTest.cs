using AutoFixture;
using Backend.Domain.Tests;
using Firebase.Auth;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = FireDetection.Backend.Domain.Entity.User;

namespace Backend.Infrastructure.Tests.Service
{
    public class UserServiceTest : SetupTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService(_unitOfWork.Object, _mapperConfig, _configuration.Object,_claimServiceTest.Object,_memoryCacheServiceTest.Object);

        }


        [Fact]
        public async Task<User> GetUserById_ReturnUser()
        {
            var mock = _fixture.Build<User>().CreateMany(10);
            var user = mock.FirstOrDefault();
            user.Id = Guid.NewGuid();
            var data = _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());

            mock.FirstOrDefault(x => x.Id == user.Id);

            var result = _userService.GetUserById(user.Id);

            Assert.NotNull(result);
            return result.Result;
        }


        [Fact]
        public async Task CheckStatusUser_ReturnFalse()
        {
            Guid userId = Guid.NewGuid();
            string status = "Active";
            var mock = _fixture.Build<User>().CreateMany(10);
            var userMock = mock.FirstOrDefault();
            userMock.Id = userId; userMock.Status = status;

            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());

            mock.FirstOrDefault(x => x.Id == userId && x.Status == status);

            var result = _userService.CheckUserStatus(userId, status);
            Assert.False(result.Result);
        }


        [Fact]
        public async Task CheckStatusUser_ReturnTrue()
        {
            Guid userId = Guid.NewGuid();
            string status = "NotActive";
            var mock = _fixture.Build<User>().CreateMany(10);

            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());

            mock.FirstOrDefault(x => x.Id == Guid.NewGuid() && x.Status == "hihih");

            var result = _userService.CheckUserStatus(userId, status);
            Assert.True(result.Result);
        }

        [Fact]
        public async Task ActiveUser_ReturnTrue()
        {
            await CheckStatusUser_ReturnTrue();
            var user = await GetUserById_ReturnUser();

            _unitOfWork.Setup(x => x.UserRepository.GetById(user.Id)).ReturnsAsync(user);
            user.LastModified = DateTime.UtcNow;
            user.Status = UserState.Inactive;

            _unitOfWork.Setup(x => x.UserRepository.Update(user));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            var result = _userService.ActiveUser(user.Id);
            Assert.True(result.Result);
        }

        [Fact]
        public async Task ActiveUser_ThrowsException_WhenUserAlreadyActive()
        {
            await CheckStatusUser_ReturnTrue();
            var user = await GetUserById_ReturnUser();

            _unitOfWork.Setup(x => x.UserRepository.GetById(user.Id)).ReturnsAsync(user);
            user.LastModified = DateTime.UtcNow;
            user.Status = UserState.Active;

            _unitOfWork.Setup(x => x.UserRepository.Update(user));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            await Assert.ThrowsAsync<HttpStatusCodeException>(() => _userService.ActiveUser(user.Id));
        }

        [Fact]
        public async Task CheckDuplicateEmail_ReturnTrue()
        {
            string emailText = "....@gmail.com";
            var mock = _fixture.Build<User>().CreateMany();
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());
            mock.FirstOrDefault(x => x.Email == emailText);

            var result = _userService.CheckDuplicateEmail(emailText);

            Assert.True(result.Result);

        }



        [Fact]
        public async Task CheckDuplicateEmail_ReturnFalse()
        {
            string emailText = "....@gmail.com";
            var mock = _fixture.Build<User>().CreateMany();
            mock.FirstOrDefault().Email = emailText;
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());
            mock.FirstOrDefault(x => x.Email == emailText);

            var result = _userService.CheckDuplicateEmail(emailText);

            Assert.False(result.Result);

        }


        [Fact]
        public async Task CheckDuplicatePhone_ReturnTrue()
        {
            string phoneText = "0902388451";
            var mock = _fixture.Build<User>().CreateMany();
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());
            mock.FirstOrDefault(x => x.Phone == phoneText);

            var result = _userService.CheckDuplicatePhone(phoneText);

            Assert.True(result.Result);
        }

        [Fact]
        public async Task CheckDuplicatePhone_ReturnFalse()
        {
            string phoneText = "0902388451";
            var mock = _fixture.Build<User>().CreateMany();
            mock.FirstOrDefault().Phone = phoneText;
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());
            mock.FirstOrDefault(x => x.Phone == phoneText);

            var result = _userService.CheckDuplicatePhone(phoneText);

            Assert.False(result.Result);
        }

        [Fact]
        public async Task<string> GenerateSecurityCode_ReturnXXX_001()
        {
            var emptyList = new List<User>().AsQueryable();
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(emptyList);
            // var user = emptyList.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

            //result 
            var result = _userService.GenerateSecurityCode();

            Assert.Equal("XXX_001", result.Result);
            return result.Result;   
        }


        [Fact]
        public async Task GenerateSecurityCode_ReturnXXX_003()
        {
            var mock = _fixture.Build<User>().CreateMany(2).ToList();
            _unitOfWork.Setup(x => x.UserRepository.GetAll()).ReturnsAsync(mock.AsQueryable());
            var user = mock.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            user.SecurityCode = "XXX_002";
            //result 
            var result = _userService.GenerateSecurityCode();

            Assert.Equal("XXX_003", result.Result);
        }


        [Fact]
        public async Task CreateUser_ThrowsException_WhenDuplicateEmailExists()
        {
            var mock = _fixture.Build<CreateUserRequest>().Create();

            await CheckDuplicateEmail_ReturnTrue();
            await  CheckDuplicatePhone_ReturnTrue();

            User userTest = _mapperConfig.Map<User>(mock);
            userTest.SecurityCode =  await GenerateSecurityCode_ReturnXXX_001();
           
            //var check = _mapperConfig.Map<UserInformationResponse>(_unitOfWork.Setup(x => x.UserRepository.Where(x => x.Id == )).Returns(userTest));
            var result = _userService.CreateUser(mock);

            Assert.NotNull(result);

        }
    }
}
