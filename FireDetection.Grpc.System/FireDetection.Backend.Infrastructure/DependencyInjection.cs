﻿using FireDetection.Backend.API.Mapper;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Repository.Repositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure
{
    public  static class DependencyInjection
    {
        public  static IServiceCollection AddInfrastructuresService(this IServiceCollection services,string databaseConnection)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecordProcessRepository, RecordProcessRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IMediaRecordRepository, MediaRecordRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<IControlCameraRepository, ControlCameraRepository>();
            services.AddScoped<IAlarmRateRepository, AlarmRateRepository>();
            services.AddScoped<IAlarmRepository , AlarmRepository>();

            services.AddScoped<IAlarmService, AlarmService>();
            services.AddScoped<ICameraService, CameraService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMediaRecordService, MediaRecordService>();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IRecordService,RecordService>();
            services.AddScoped<ITimerService , TimerService>();
            services.AddScoped<IUserService , UserService>();


           services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;


            

        }
    }
}
