using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.EntitySetting;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private static FireDetectionDbContext _context;
        protected DbSet<T> _dbSet;
        public GenericRepository(FireDetectionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
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

        public DbSet<T> GetQuery()
        {
            return _dbSet;
        }

        public void HardDelete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public  async Task InsertAsync(T obj)
        {
            await  _dbSet.AddAsync(obj);
        }

        public void SoftDelete(T obj)
        {
            _dbSet.Update(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
