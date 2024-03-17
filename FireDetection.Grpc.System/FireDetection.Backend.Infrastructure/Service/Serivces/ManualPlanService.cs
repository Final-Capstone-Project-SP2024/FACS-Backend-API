using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class ManualPlanService : IManualPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManualPlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IQueryable<ManualPlanGeneralResponse>> GetAll()
        {
            List<ManualPlan> data = await _unitOfWork.ManualPlanRepository.GetAll();
            Console.WriteLine(data);
            List<ManualPlanGeneralResponse> result = new List<ManualPlanGeneralResponse>();
            foreach (var item in data)
            {
             result.Add(_mapper.Map<ManualPlanGeneralResponse>(item));     
            }
            return result.AsQueryable();
        }

        public async Task<ManualPlanDetailResponse> GetManualPlanDetail(int id)
        {
          return _mapper.Map<ManualPlanDetailResponse>(_unitOfWork.ManualPlanRepository.GetDetail(id));
        }

        public async Task<ManualPlanDetailResponse> Update(int id, UpdateManualPlanRequest update)
        {
            var manualPlan = await _unitOfWork.ManualPlanRepository.GetDetail(id);
            _mapper.Map(manualPlan, update);

            await _unitOfWork.ManualPlanRepository.Update(manualPlan);

            return  _mapper.Map<ManualPlanDetailResponse>(_unitOfWork.ManualPlanRepository.GetDetail(id));
        }
    }
}
