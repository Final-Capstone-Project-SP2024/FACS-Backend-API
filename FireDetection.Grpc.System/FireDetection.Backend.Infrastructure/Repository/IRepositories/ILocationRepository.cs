using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public Task<ReadOnlyCollection<Guid>> GetStaffInLocation(Guid locationId);
    }
}
