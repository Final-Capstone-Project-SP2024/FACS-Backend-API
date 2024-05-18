using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
//using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<AlarmConfiguration>>> UpdateAlarmConfiguration(int id,AddAlarmConfigurationRequest request)
        {
           if(id == 1 )
            {
               if(request.Start != 0 || request.End > 40) {
                    return BadRequest(new
                    {
                        message = "Must in O-40"
                    });
                }
            }
            if (id == 2)
            {
                if (request.Start < 40 || request.End != 100)
                {
                    return BadRequest(new
                    {
                        message = "Must in 40-100"
                    });
                }
            }
            if (!ModelState.IsValid)
            {
                
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);

            }
            await _alarmConfigurationService.UpdateAlarmConfiguration(id, request);
            return new RestDTO<AlarmConfiguration>()
            {
                Message = "Update Alarm Coonfiguration Sucessfully",
                Data = null,
            };
        }
    }
}

