using FireDetection.Backend.Domain.DTOs.Request;
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
        public async Task<ActionResult> GetAlarmConfiguration()
        {
            return Ok(_alarmConfigurationService.GetAlarmConfigurations());
        }

        [HttpPost]
        public async Task<ActionResult> AddAlarmConfiguration(AddAlarmConfigurationRequest request)
        {
            await _alarmConfigurationService.AddNewConfiguration(request);
            return Ok("Create Sucessfully");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAlarmConfiguration(int id,AddAlarmConfigurationRequest request)
        {
            await _alarmConfigurationService.UpdateAlarmConfiguration(id, request);
            return Ok("Update Sucessfully");
        }
    }
}

