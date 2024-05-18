using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IActionService
    {
        public Task<List<ActionConfigurationResponse>> Get();

        public Task Update(int ActionConfigId, UpdateActionConfigRequest request);
    }
}
