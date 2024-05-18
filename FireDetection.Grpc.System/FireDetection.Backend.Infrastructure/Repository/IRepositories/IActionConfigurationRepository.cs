using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IActionConfigurationRepository 
    {
        public Task<List<ActionConfigurationResponse>> GetActionConfig();

        public Task  UpdateActionConfig(int actionId,UpdateActionConfigRequest request);
    }
}
