using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using FireDetection.Backend.API.Mapper;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
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
        protected readonly Mock<ILocationRepository> _locationRepository;
        protected readonly Mock<ILocationService> _locationService;

        protected readonly Mock<IUserRepository> _userRepository;
        protected readonly Mock<IUserService> _userService;

        protected readonly Mock<IAlarmRepository> _alarmRepository;
        protected readonly Mock<IAlarmService> _alarmService;

        protected readonly Mock<IRecordProcessRepository> _recordProcessRepository;

        protected readonly Mock<IRecordRepository> _recordRepository;

        protected readonly Mock<ICameraRepository> _cameraRepository;
        protected readonly Mock<ICameraService> _cameraService;

        protected readonly Mock<IControlCameraRepository> _controlCameraRepository;
        protected readonly Mock<IAlarmRateRepository> _alarmrateRepository;
        protected readonly Mock<IRecordService> _recordService;

        protected readonly Mock<IMediaRecordRepository> _mediaRecordRepository;
        protected readonly Mock<IMediaRecordService> _mediaRecordService;

        protected readonly Mock<ITimerService> _timerService;
        protected readonly Mock<IMemoryCacheService> _memoryCacheService;

        protected readonly Mock<LinkGenerator> _linkGenerator;
        protected readonly FireDetectionDbContext _dbContext;

        public SetupTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();


            _linkGenerator = new Mock<LinkGenerator>();
            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));

            _unitOfWork = new Mock<IUnitOfWork>();
            _locationRepository = new Mock<ILocationRepository>();
            _locationService = new Mock<ILocationService>();
            _alarmrateRepository = new Mock<IAlarmRateRepository>();
            _alarmRepository = new Mock<IAlarmRepository>();
            _memoryCacheService = new Mock<IMemoryCacheService>();
            _timerService = new Mock<ITimerService>();
            _mediaRecordService = new Mock<IMediaRecordService>();
            _mediaRecordRepository = new Mock<IMediaRecordRepository>();
            _recordService = new Mock<IRecordService>();
            _cameraRepository = new Mock<ICameraRepository>();
            _cameraService = new Mock<ICameraService>();
            _userRepository = new Mock<IUserRepository>();
            _userService = new Mock<IUserService>();
            _controlCameraRepository = new Mock<IControlCameraRepository>();
            _alarmService = new Mock<IAlarmService>();
            _recordRepository = new Mock<IRecordRepository>();
            _recordProcessRepository = new Mock<IRecordProcessRepository>();


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
