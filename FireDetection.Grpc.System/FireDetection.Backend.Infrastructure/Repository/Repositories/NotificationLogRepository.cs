using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class NotificationLogRepository : GenericRepository<NotificationLog>, INotificationLogRepository
    {
        private readonly FireDetectionDbContext _context;

        public NotificationLogRepository(FireDetectionDbContext context) : base(context) 
        {
            _context = context;
        }

      
    }
}
