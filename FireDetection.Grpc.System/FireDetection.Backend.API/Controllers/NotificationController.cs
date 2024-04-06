using FireDetection.Backend.API.Middleware;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]

    public class NotificationController : BaseController
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IRecordService _recordService;
        public NotificationController(LinkGenerator linkGenerator, IRecordService recordService)
        {
            _linkGenerator = linkGenerator;
            _recordService = recordService;
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        public async Task<IActionResult> Add(AddNotificationRequest request)
        {
            await NotificationHandler.AddNewNotification(request.Title, request.Context, request.Header);
            return Ok("Add Sucessfully ");

        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<NotificationListResponse>>> GetAll()
        {
            return new RestDTO<NotificationListResponse>()
            {
                Message = "Get Notifications Successfully",
                Data = await NotificationHandler.GetAll(),
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RestDTO<NotficationDetailResponse>>> GetDetail(int id)
        {
            return new RestDTO<NotficationDetailResponse>()
            {
                Message = "Get Notifications Successfully",
                Data = await NotificationHandler.Get(id),
            };
        }

        [HttpGet("/firealarms")]
        public async Task<ActionResult<RestDTO<IEnumerable<NotificationAlarmResponse>>>> GetNotify()
        {
            var response = await _recordService.GetNotificationAlarm();
            if (response != null)
            {
                return new RestDTO<IEnumerable<NotificationAlarmResponse>>()
                {
                    Message = "Get Notifications Successfully",
                    Data = await _recordService.GetNotificationAlarm()
                };
            }else
            {
                return new RestDTO<IEnumerable<NotificationAlarmResponse>>()
                {
                    Message = "Don't have any Alarm",
                    Data = null
                };
            }


        }
    }
}
