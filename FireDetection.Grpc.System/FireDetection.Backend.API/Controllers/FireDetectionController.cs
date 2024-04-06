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
    [ApiController]
    public class FireDetectionController : BaseController
    {
        private readonly INotificationLogService _notificationLogService;
        public FireDetectionController(INotificationLogService notificationLogService)
        {
            _notificationLogService = notificationLogService;
        }
        //todo get analysis about system to view in dashboard
        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<FireDetectionAnalysis>>> Get()
        {
            return new RestDTO<FireDetectionAnalysis>()
            {
                Message = "Get data analysis  Successfully!",
                Data = await _notificationLogService.Analysis(),
            };
        }



    }
}
