using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface INotificationLogRepository : IGenericRepository<NotificationLog>
    {
        Task<List<RecordInDay>> GetDayAnalysis();

        Task<List<RecordInMonth>> GetMonthAnalysis();

        Task<List<RecordInYear>> GetYearAnalysis();

    }
}
