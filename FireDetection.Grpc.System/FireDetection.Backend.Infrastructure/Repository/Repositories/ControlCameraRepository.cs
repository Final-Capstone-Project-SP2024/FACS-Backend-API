using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class ControlCameraRepository : GenericRepository<ControlCamera> , IControlCameraRepository
    {
        private readonly FireDetectionDbContext _context;
        public ControlCameraRepository(FireDetectionDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
