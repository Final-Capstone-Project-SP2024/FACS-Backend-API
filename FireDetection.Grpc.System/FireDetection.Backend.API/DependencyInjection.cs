﻿using AutoMapper;
using FireDetection.Backend.API.Mapper;
using FireDetection.Backend.API.Middleware.GraphQL;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Repository.Repositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using FireDetection.Backend.Infrastructure.Service.Serivces.Future;
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
            //Record Process
            services.AddScoped<IRecordProcessRepository, RecordProcessRepository>();

            //Record 
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IAPICallService, APICallService>();
            //Alarm 
            services.AddScoped<IAlarmService, AlarmService>();
            services.AddScoped<IAlarmRepository, AlarmRepository>();
            //Location
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            //Camera
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<ICameraService, CameraService>();

            services.AddScoped<IControlCameraRepository, ControlCameraRepository>();
            //Alarm Rate
            services.AddScoped<IAlarmRateRepository, AlarmRateRepository>();
            services.AddScoped<IRecordService, RecordService>();
            // Record
            //services.AddScoped<IRecordRepository, RecordRepository>();
            //services.AddScoped<IRecordS, CameraService>();

            //Media Record
            services.AddScoped<IMediaRecordRepository, MediaRecordRepository>();
            services.AddScoped<IMediaRecordService, MediaRecordService>();

            services.AddScoped<ITimerService, TimerService>();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();

            services.AddScoped<IClaimsService, ClaimsService>();

            services.AddScoped<IBugsReportRepository, BugsReportRepository>();
            services.AddScoped<IBugsReportService, BugsReportService>();

            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();    

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService ,TransactionService>();


            services.AddScoped<IManualPlanRepository, ManualPlanRepository>();
            services.AddScoped<IManualPlanService, ManualPlanService>();


            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractService, ContractService>();



            services.AddGraphQLServer()
                    .AddQueryType<Query>()
                    .AddProjections()
                    .AddFiltering()
                    .AddSorting();
            return services;
        }
    }
}
