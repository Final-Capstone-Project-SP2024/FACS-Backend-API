using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IManualPlanService
    {
        public Task<IQueryable<ManualPlanGeneralResponse>> GetAll();

        public Task<ManualPlanDetailResponse> GetManualPlanDetail(int id);

        public Task<ManualPlanDetailResponse> Update(int id, UpdateManualPlanRequest update);
    }
}
