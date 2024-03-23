using FireDetection.Backend.API.Middleware;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Helpers.Media;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]

    public class NotificationController : BaseController
    {
        private readonly LinkGenerator _linkGenerator;
        public NotificationController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
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


        [HttpPost("/upload")]
        public async Task<IActionResult> TestUpload(IFormFile fileUpload)
        {
              //await StorageHandlers.UploadFileAsync(fileUpload, "video");
           string result = VideoConverterHandler.SaveAviFile(fileUpload);
            return Ok($"Upload successfull ");
        }
    }
}
