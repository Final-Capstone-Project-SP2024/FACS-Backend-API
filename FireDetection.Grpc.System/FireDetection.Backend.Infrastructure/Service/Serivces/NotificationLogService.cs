using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class NotificationLogService : INotificationLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveNotificationVotingRequire(Guid recordId, int count)
        {
            NotificationLog log = new NotificationLog
            {
                Count = count,
                RecordId = recordId,
                NotificationTypeId = 2
            };

            _unitOfWork.NotificationLogRepository.InsertAsync(log);
        }

        public async Task SaveNotificationFireNotifyLog(Guid recordId, int count)
        {
            NotificationLog log = new NotificationLog
            {
                Count = count,
                RecordId = recordId,
                NotificationTypeId = 1
            };

            _unitOfWork.NotificationLogRepository.InsertAsync(log);
            await _unitOfWork.SaveChangeAsync();


        }

        public void SaveNotificationActionRequire(Guid recordId, int count, CacheType type)
        {
            NotificationLog log = new NotificationLog
            {
                Count = count,
                RecordId = recordId,
                NotificationTypeId = TransferType(type)
            };

            _unitOfWork.NotificationLogRepository.InsertAsync(log);

        }

        private static int TransferType(CacheType type) => type switch
        {
            CacheType.AlarmLevel1 => 3,
            CacheType.AlarmLevel2 => 4,
            CacheType.AlarmLevel3 => 5,
            CacheType.AlarmLevel4 => 6,
            CacheType.AlarmLevel5 => 7,

        };

        public async  Task<FireDetectionAnalysis> Analysis()
        {
            FireDetectionAnalysis analysis = new FireDetectionAnalysis();
            analysis.CountFireAlarmLevel1 = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 3).Count();
            analysis.CountFireAlarmNotification = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 1).Count();
            analysis.CountFireAlarmLevel2 = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 4).Count();
            analysis.CountFireAlarmLevel3 = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 5).Count();
            analysis.CountFireAlarmLevel4 = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 6).Count();
            analysis.CountFireAlarmLevel5 = _unitOfWork.NotificationLogRepository.Where(x => x.NotificationTypeId == 7).Count();
            analysis.HighRiskFireArea =  await _unitOfWork.CameraRepository.HighRiskFireDetectByCamera();

            return analysis;
        }
    }
}
