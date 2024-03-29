﻿using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FireDetectionDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IBugsReportRepository _bugsReportRepository;
        private readonly IAlarmRateRepository _alarmRateRepository;
        private readonly IRecordProcessRepository _processRepository;
        private readonly IMediaRecordRepository _mediaRecordRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICameraRepository _cameraRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IControlCameraRepository _controlCameraRepository;
        private readonly IAlarmRepository _alarmRepository;
        private readonly INotificationLogRepository _notificationLogRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IManualPlanRepository _manualPlanRepository;
        private readonly ITransactionRepository _transactionRepository;

        public UnitOfWork(FireDetectionDbContext context,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            ICameraRepository cameraRepository,
            IMediaRecordRepository mediaRecordRepository,
            IRecordRepository recordRepository,
            IControlCameraRepository controlCameraRepository,
            IAlarmRepository alarmRepository,
            IRecordProcessRepository processRepository,
            IAlarmRateRepository alarmRateRepository,
            INotificationLogRepository notificationLogRepository,
            IFeedbackRepository feedbackRepository,
            IBugsReportRepository bugsReportRepository,
            IContractRepository contractRepository,
            IManualPlanRepository manualPlanRepository,
            ITransactionRepository transactionRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _cameraRepository = cameraRepository;
            _mediaRecordRepository = mediaRecordRepository;
            _recordRepository = recordRepository;
            _controlCameraRepository = controlCameraRepository;
            _alarmRepository = alarmRepository;
            _processRepository = processRepository;
            _alarmRateRepository = alarmRateRepository;
            _notificationLogRepository = notificationLogRepository;
            _feedbackRepository = feedbackRepository;
            _bugsReportRepository = bugsReportRepository;
            _contractRepository = contractRepository;
            _manualPlanRepository = manualPlanRepository;
            _transactionRepository = transactionRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public IAlarmRateRepository AlarmRateRepository => _alarmRateRepository;
        public ILocationRepository LocationRepository => _locationRepository;

        public ICameraRepository CameraRepository => _cameraRepository;
        public IRecordProcessRepository RecordProcessRepository => _processRepository;
        public IRecordRepository RecordRepository => _recordRepository;
        public IControlCameraRepository ControlCameraRepository => _controlCameraRepository;
        public IMediaRecordRepository MediaRecordRepository => _mediaRecordRepository;  

        public IAlarmRepository AlarmRepository => _alarmRepository;

        public INotificationLogRepository NotificationLogRepository => _notificationLogRepository;

        public IBugsReportRepository BugsReportRepository => _bugsReportRepository;

        public IFeedbackRepository FeedbackRepository => _feedbackRepository;

        public IContractRepository ContractRepository => _contractRepository;

        public IManualPlanRepository ManualPlanRepository => _manualPlanRepository;

        public ITransactionRepository TransactionRepository => _transactionRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
