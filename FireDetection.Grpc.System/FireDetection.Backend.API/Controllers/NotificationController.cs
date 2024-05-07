using FireDetection.Backend.API.Middleware;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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


        [HttpPost]
        public async Task<IActionResult> Add(AddNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            await NotificationHandler.AddNewNotification(request.Title, request.Context, request.Header);
            return Ok(new
            {
                message = "Add New Sucessfully"
            });

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

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, UpdateNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            await NotificationHandler.UpdateNotification(id, request.Title, request.Context);
            return Ok(new
            {
                message = "Add Sucessfully "
            });
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


        [Authorize(Roles = Roles.Manager + "," + Roles.User)]
        [HttpGet("firealarms")]
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
            }
            else
            {
                return new RestDTO<IEnumerable<NotificationAlarmResponse>>()
                {
                    Message = "Don't have any Alarm",
                    Data = null
                };
            }


        }


        [HttpGet("disconnectedalarms")]
        public async Task<ActionResult<RestDTO<IEnumerable<NotificationAlarmResponse>>>> GetDisconnectedNotify()
        {

            return new RestDTO<IEnumerable<NotificationAlarmResponse>>()
            {
                Message = "Get Notifications Successfully",
                Data = await _recordService.GetNotificationAlarm()
            };
        }
    }
}
