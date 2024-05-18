using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ActionConfigurationResponse>> Get()
        {
            return await _unitOfWork.ActionConfigurationRepository.GetActionConfig();
        }

        public async Task Update(int ActionConfigId, UpdateActionConfigRequest request)
        {
            await _unitOfWork.ActionConfigurationRepository.UpdateActionConfig(ActionConfigId, request);    
        }
    }
}
