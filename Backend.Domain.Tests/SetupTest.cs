using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using FireDetection.Backend.API.Mapper;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Tests
{
    public class SetupTest : IDisposable
    {
        protected readonly Fixture _fixture;
        protected readonly IMapper _mapperConfig;
        protected readonly Mock<IUnitOfWork> _unitOfWork;
        protected readonly Mock<ILocationRepository> _locationRepositoryTest;
        protected readonly Mock<ILocationService> _locationServiceTest;
        protected readonly Mock<IConfiguration> _configuration;
        protected readonly Mock<IHttpContextAccessor> _mockContextAccessorTest;
        protected readonly Mock<HttpRequest> _mockHttpRequestTest;

        protected readonly Mock<HttpContext> _httpContextTest;
        protected readonly Mock<IUrlHelper> _urlHelperTest;
        protected readonly Mock<IUserRepository> _userRepositoryTest;
        protected readonly Mock<IUserService> _userServiceTest;

        protected readonly Mock<IAlarmRepository> _alarmRepositoryTest;
        protected readonly Mock<IAlarmService> _alarmServiceTest;

        protected readonly Mock<IRecordProcessRepository> _recordProcessRepositoryTest;

        protected readonly Mock<IRecordRepository> _recordRepositoryTest;

        protected readonly Mock<ICameraRepository> _cameraRepositoryTest;
        protected readonly Mock<ICameraService> _cameraServiceTest;

        protected readonly Mock<IControlCameraRepository> _controlCameraRepositoryTest;
        protected readonly Mock<IAlarmRateRepository> _alarmrateRepositoryTest;
        protected readonly Mock<INotificationLogRepository> _notificationLogRepositoryTest;

        protected readonly Mock<IRecordService> _recordServiceTest;

        protected readonly Mock<IMediaRecordRepository> _mediaRecordRepositoryTest;
        protected readonly Mock<IMediaRecordService> _mediaRecordServiceTest;

        protected readonly Mock<ITimerService> _timerServiceTest;
        protected readonly Mock<IMemoryCacheService> _memoryCacheServiceTest;

        protected readonly Mock<LinkGenerator> _linkGeneratorTest;
        protected readonly FireDetectionDbContext _dbContext;

        public SetupTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();


            _linkGeneratorTest = new Mock<LinkGenerator>();
            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));

            _configuration = new Mock<IConfiguration>();

            _mockHttpRequestTest = new Mock<HttpRequest>();
            _mockContextAccessorTest = new Mock<IHttpContextAccessor>();
            _httpContextTest = new Mock<HttpContext>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _locationRepositoryTest = new Mock<ILocationRepository>();
            _locationServiceTest = new Mock<ILocationService>();
            _alarmrateRepositoryTest = new Mock<IAlarmRateRepository>();
            _alarmRepositoryTest = new Mock<IAlarmRepository>();
            _memoryCacheServiceTest = new Mock<IMemoryCacheService>();
            _timerServiceTest = new Mock<ITimerService>();
            _mediaRecordServiceTest = new Mock<IMediaRecordService>();
            _mediaRecordRepositoryTest = new Mock<IMediaRecordRepository>();
            _recordServiceTest = new Mock<IRecordService>();
            _cameraRepositoryTest = new Mock<ICameraRepository>();
            _cameraServiceTest = new Mock<ICameraService>();
            _userRepositoryTest = new Mock<IUserRepository>();
            _userServiceTest = new Mock<IUserService>();
            _controlCameraRepositoryTest = new Mock<IControlCameraRepository>();
            _alarmServiceTest = new Mock<IAlarmService>();
            _recordRepositoryTest = new Mock<IRecordRepository>();
            _recordProcessRepositoryTest = new Mock<IRecordProcessRepository>();

            _urlHelperTest = new Mock<IUrlHelper>();
            var options = new DbContextOptionsBuilder<FireDetectionDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new FireDetectionDbContext(options);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
