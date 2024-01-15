using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class CameraRepository : GenericRepository<Camera>, ICameraRepository
    {
        private readonly FireDetectionDbContext _context;
        public CameraRepository(FireDetectionDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
