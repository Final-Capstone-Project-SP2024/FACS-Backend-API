using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly FireDetectionDbContext _context;

        public LocationRepository(FireDetectionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReadOnlyCollection<Guid>> GetStaffInLocation(Guid locationId)
        {

            var result = _context.ControlCameras
                           .Include(x => x.User)
                           .Where(cc => cc.LocationID == locationId)
                           .Select(cc => cc.UserID)
                           .ToList().AsReadOnly();

            return result;
        }
    }
}
