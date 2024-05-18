using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    public class ActionConfigurationController : BaseController
    {
        private readonly IActionService _actionService;
        public ActionConfigurationController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet]
        public async Task<ActionResult<RestDTO<List<ActionConfigurationResponse>>>> Get()
        {
            return new RestDTO<List<ActionConfigurationResponse>>()
            {
                Message = "Alarm Action Setting",
                Data = await _actionService.Get()
            };
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, UpdateActionConfigRequest request)
        {
           await _actionService.Update(id, request);

            return Ok(new
            {
                message = "Update Sucessfully"
            }) ;
              
        }
    }
}
