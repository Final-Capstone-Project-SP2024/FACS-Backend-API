using FireDetection.Backend.Domain;
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
        private readonly IRecordProcessRepository _processRepository;
        private readonly IMediaRecordRepository _mediaRecordRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICameraRepository _cameraRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IControlCameraRepository _controlCameraRepository;
        private readonly INotificationLogRepository _notificationLogRepository;
        private readonly IAlarmConfigurationRepository _alarmConfigurationRepository;
        private readonly IUserResponsibilityRepository _userResponsibilityRepository;
        private readonly IActionConfigurationRepository _actionConfigurationRepository;

        public UnitOfWork(FireDetectionDbContext context,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            ICameraRepository cameraRepository,
            IMediaRecordRepository mediaRecordRepository,
            IRecordRepository recordRepository,
            IControlCameraRepository controlCameraRepository,
            IRecordProcessRepository processRepository,
            INotificationLogRepository notificationLogRepository,
            IAlarmConfigurationRepository alarmConfigurationRepository,
            IUserResponsibilityRepository userResponsibilityRepository,
            IActionConfigurationRepository actionConfigurationRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _cameraRepository = cameraRepository;
            _mediaRecordRepository = mediaRecordRepository;
            _recordRepository = recordRepository;
            _controlCameraRepository = controlCameraRepository;
            _processRepository = processRepository;
            _notificationLogRepository = notificationLogRepository;
            _alarmConfigurationRepository = alarmConfigurationRepository;
            _userResponsibilityRepository = userResponsibilityRepository;
            _actionConfigurationRepository = actionConfigurationRepository;
        }
        public IUserRepository UserRepository => _userRepository;
        public ILocationRepository LocationRepository => _locationRepository;
        public ICameraRepository CameraRepository => _cameraRepository;
        public IRecordProcessRepository RecordProcessRepository => _processRepository;
        public IRecordRepository RecordRepository => _recordRepository;
        public IControlCameraRepository ControlCameraRepository => _controlCameraRepository;
        public IMediaRecordRepository MediaRecordRepository => _mediaRecordRepository;  
        public INotificationLogRepository NotificationLogRepository => _notificationLogRepository;

        public IAlarmConfigurationRepository AlarmConfigurationRepository => _alarmConfigurationRepository;

        public IUserResponsibilityRepository UserResponsibilityRepository => _userResponsibilityRepository;

        public IActionConfigurationRepository ActionConfigurationRepository => _actionConfigurationRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
