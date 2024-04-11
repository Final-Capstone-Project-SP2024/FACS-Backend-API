using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FireDetection.Backend.API.Controllers
{
    public class FireDetectionController : BaseController
    {
        private readonly INotificationLogService _notificationLogService;
        public FireDetectionController(INotificationLogService notificationLogService)
        {
            _notificationLogService = notificationLogService;
        }
        //todo get analysis about system to view in dashboard
        //[Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<FireDetectionAnalysis>>> Get()
        {
            return new RestDTO<FireDetectionAnalysis>()
            {
                Message = "Get data analysis  Successfully!",
                Data = await _notificationLogService.Analysis(),
            };
        }


        [HttpGet("day")]
        public async Task<ActionResult> GetDay()
        {

            return Ok(await _notificationLogService.GetInDay());
        }

        [HttpGet("month")]
        public async Task<ActionResult> GetMonth()
        {

            return Ok(await _notificationLogService.GetInMonth());
        }


        [HttpGet("year")]
        public async Task<ActionResult> GetYear()
        {

            return Ok(await _notificationLogService.GetInYear());
        }

        [HttpGet("week")]
        public async Task<ActionResult> GetWeek()
        {
            return Ok(await _notificationLogService.GetInWeek());
        }


        //todo data for pie chart analysis 
        [HttpGet("locationAnalysis")]
        public async Task<ActionResult> GetLocationAnalysis()
        {
            return Ok(await _notificationLogService.GetLocationAnalysis());
        }

    }
}
