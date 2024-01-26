using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Guid>> GetUsersByCameraId(Guid cameraId)
        {
            var result = _context.Cameras
                                .Where(c => c.Id == cameraId)
                                .SelectMany(c => c.Location.ControlCameras.Select(cc => cc.UserID))
                                .ToList();

            return result;
        }
    }
}
