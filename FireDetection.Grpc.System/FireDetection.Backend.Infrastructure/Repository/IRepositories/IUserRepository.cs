using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<IEnumerable<UserInformationResponse>> GetUsersUnRegisterd(Guid LocationId);
    }
}
