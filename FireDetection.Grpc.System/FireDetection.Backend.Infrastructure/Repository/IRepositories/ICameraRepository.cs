using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface ICameraRepository : IGenericRepository<Camera>
    {

        public  Task<List<Guid>> GetUsersByCameraId(Guid cameraId);
    }
}
