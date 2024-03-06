using FireDetection.Backend.API.Middleware;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [ApiKey]
    public class NotificationController : BaseController
    {
        private readonly LinkGenerator _linkGenerator;
        public NotificationController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        //[Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        public async Task<IActionResult> Add(AddNotificationRequest request)
        {
            await NotificationHandler.AddNewNotification(request.Title, request.Context, request.Header);
            return Ok("Add Sucessfully ");

        }

//        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<NotificationListResponse>>> GetAll()
        {
            return new RestDTO<NotificationListResponse>()
            {
                Message = "Get Notifications Successfully",
                Data = await NotificationHandler.GetAll(),
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetAll),"/NotificationController",
                        "",
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }

      //  [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RestDTO<NotficationDetailResponse>>> GetDetail(int id)
        {
            return new RestDTO<NotficationDetailResponse>()
            {
                Message = "Get Notifications Successfully",
                Data = await NotificationHandler.Get(id),
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetDetail),"/NotificationController",
                        id,
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }
    }
}
