using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
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

        public  async Task<IQueryable<CameraInformationResponse>> GetAllViewModel()
        {
            return GetCameras(_context).AsQueryable();
        }

        private static readonly Func<FireDetectionDbContext, IEnumerable<CameraInformationResponse>>
            GetCameras =
            EF.CompileQuery(
                (FireDetectionDbContext context) =>
                context.Cameras.Select(x => new CameraInformationResponse
                {
                    CameraDestination = x.CameraDestination,
                    CameraId = x.Id,
                    CameraName = x.CameraName,
                    Status = x.Status
                }));
    }
}
