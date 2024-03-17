using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IManualPlanRepository 
    {
        public  Task<List<ManualPlan>> GetAll();

        public  Task<ManualPlan> GetDetail(int id);


        public Task Update(ManualPlan manualPlan);
    }
}
