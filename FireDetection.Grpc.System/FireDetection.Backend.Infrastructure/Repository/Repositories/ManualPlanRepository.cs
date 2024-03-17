using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class ManualPlanRepository : IManualPlanRepository
    {
        private readonly FireDetectionDbContext _dbContext;

        public ManualPlanRepository(FireDetectionDbContext dbContext) 
        {
                _dbContext = dbContext;
        }

        public  async Task<List<ManualPlan>> GetAll()
        {
          return  await _dbContext.ManualPlans.ToListAsync();
            
        }

        public  async Task<ManualPlan> GetDetail(int id)
        {
            return   _dbContext.ManualPlans.FirstOrDefault(x => x.ManualPlanId == id);
        }

        public async Task Update(ManualPlan manualPlan)
        {
            _dbContext.ManualPlans.Update(manualPlan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
