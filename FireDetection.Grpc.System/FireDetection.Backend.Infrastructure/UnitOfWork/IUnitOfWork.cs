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
        public IAlarmConfigurationRepository AlarmConfigurationRepository { get; }
        public IAlarmRateRepository AlarmRateRepository { get; }
        public IRecordProcessRepository RecordProcessRepository { get; }
        public IUserRepository UserRepository { get; }
        public IMediaRecordRepository MediaRecordRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public ICameraRepository CameraRepository { get; }
        public IRecordRepository RecordRepository { get; }
        public IAlarmRepository AlarmRepository { get; }
        public IControlCameraRepository ControlCameraRepository { get; }
        public INotificationLogRepository NotificationLogRepository { get; }

        public IBugsReportRepository BugsReportRepository { get; }

        public IFeedbackRepository FeedbackRepository { get; }

        public IContractRepository ContractRepository { get; }

        public IManualPlanRepository ManualPlanRepository { get; }

        public ITransactionRepository TransactionRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
