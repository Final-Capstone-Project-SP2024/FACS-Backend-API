using AutoMapper;
using FireDetection.Backend.API.Mapper;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Repository.Repositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FireDetection.Backend.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>(); 
            
            //Alarm 
            services.AddScoped<IAlarmService ,AlarmService>();
            services.AddScoped<IAlarmRepository, AlarmRepository>();
            //Location
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationRepository,LocationRepository>();
            //Camera
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<ICameraService, CameraService>();

            services.AddScoped<IControlCameraRepository, ControlCameraRepository>();

            // Record
            services.AddScoped<IRecordRepository, RecordRepository>();
            //services.AddScoped<IRecordS, CameraService>();

            //Media Record
            services.AddScoped<IMediaRecordRepository, MediaRecordRepository>();
            //services.AddScoped<IMediaRecordS, MediaRecordRepository>();
            return services;
        }
    }
}
