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
        private readonly IMediaRecordRepository _mediaRecordRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICameraRepository _cameraRepository;
        private readonly IRecordRepository _recordRepository;
        private readonly IControlCameraRepository _controlCameraRepository;
        private readonly IAlarmRepository _alarmRepository;


        public UnitOfWork(FireDetectionDbContext context,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            ICameraRepository cameraRepository,
            IMediaRecordRepository mediaRecordRepository,
            IRecordRepository recordRepository,
            IControlCameraRepository controlCameraRepository,
            IAlarmRepository alarmRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _cameraRepository = cameraRepository;
            _mediaRecordRepository = mediaRecordRepository;
            _recordRepository = recordRepository;
            _controlCameraRepository = controlCameraRepository;
            _alarmRepository = alarmRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public ILocationRepository LocationRepository => _locationRepository;

        public ICameraRepository CameraRepository => _cameraRepository;

        public IRecordRepository RecordRepository => _recordRepository;
        public IControlCameraRepository ControlCameraRepository => _controlCameraRepository;
        public IMediaRecordRepository MediaRecordRepository => _mediaRecordRepository;  

        public IAlarmRepository AlarmRepository => _alarmRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
