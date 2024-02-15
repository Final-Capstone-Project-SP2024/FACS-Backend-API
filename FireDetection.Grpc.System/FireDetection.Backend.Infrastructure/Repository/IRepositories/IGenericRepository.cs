using FireDetection.Backend.Domain.EntitySetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task InsertAsync(T obj);
        void Update(T obj);
        void SoftDelete(T obj);
        void HardDelete(T obj);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        DbSet<T> GetQuery();
    }
}
