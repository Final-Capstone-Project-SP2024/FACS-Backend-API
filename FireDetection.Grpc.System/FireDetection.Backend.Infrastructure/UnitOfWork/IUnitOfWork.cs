using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserResponsibilityRepository UserResponsibilityRepository { get; }
        public IAlarmConfigurationRepository AlarmConfigurationRepository { get; }
        public IRecordProcessRepository RecordProcessRepository { get; }
        public IUserRepository UserRepository { get; }
        public IMediaRecordRepository MediaRecordRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public ICameraRepository CameraRepository { get; }
        public IRecordRepository RecordRepository { get; }
        public IControlCameraRepository ControlCameraRepository { get; }
        public INotificationLogRepository NotificationLogRepository { get; }

        public IActionConfigurationRepository ActionConfigurationRepository { get; }


        public Task<int> SaveChangeAsync();
    }
}
