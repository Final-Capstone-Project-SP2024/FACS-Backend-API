using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public NotificationController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddNotificationRequest request)
        {
            await NotificationHandler.AddNewNotification(request.Title, request.Context, request.Header);
            return Ok("Add Sucessfully ");

        }



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
