using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.EntitySetting;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _dbSet;
        public GenericRepository(FireDetectionDbContext context )
        {
         _dbSet  = context.Set<T>();   
        }
        public async Task<IQueryable<T>> GetAll()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task<T?> GetById(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public void HardDelete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public async void InsertAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public void SoftDelete(T obj)
        {
            _dbSet.Update(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }
    }
}
