using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
//using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    
    public class AlarmConfigurationController : BaseController
    {
        private readonly IAlarmConfigurationService _alarmConfigurationService;
        public AlarmConfigurationController(IAlarmConfigurationService alarmConfigurationService)
        {
            _alarmConfigurationService = alarmConfigurationService;
        }

        [HttpGet]
        public async Task<ActionResult<RestDTO<IEnumerable<AlarmConfiguration>>>> GetAlarmConfiguration()
        {
            return new RestDTO<IEnumerable<AlarmConfiguration>>()
            {
                Message = "Alarm Configuration Setting",
                Data = await _alarmConfigurationService.GetAlarmConfigurations(),
            };
        }

        [HttpPost]
        public async Task<ActionResult<RestDTO<AlarmConfiguration>>> AddAlarmConfiguration(AddAlarmConfigurationRequest request)
        {
            await _alarmConfigurationService.AddNewConfiguration(request);
            return new RestDTO<AlarmConfiguration>()
            {
                Message = "Add Alarm Coonfiguration Sucessfully",
                Data = null,
            };
        }

        [HttpPatch]
        public async Task<ActionResult<RestDTO<AlarmConfiguration>>> UpdateAlarmConfiguration(int id,AddAlarmConfigurationRequest request)
        {
            await _alarmConfigurationService.UpdateAlarmConfiguration(id, request);
            return new RestDTO<AlarmConfiguration>()
            {
                Message = "Update Alarm Coonfiguration Sucessfully",
                Data = null,
            };
        }
    }
}

