using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> GetById(Guid id);
        void InsertAsync(T obj);
        void Update(T obj);
        void SoftDelete(T obj);
        void HardDelete(T obj);
    }
}
