using AutoMapper;
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
    public class CameraController : BaseController
    {
        private readonly ICameraService _cameraService;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CameraController> _logger;
        public CameraController(ICameraService cameraService, LinkGenerator linkGenerator, ILogger<CameraController> logger)
        {
            _cameraService = cameraService;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpPost("{cameraId}/reconnect")]
        public async Task<ActionResult<RestDTO<string>>> ReconnectCamera(Guid cameraId)
        {
            if (! await _cameraService.CheckIsEnable())
            {
                return BadRequest(new
                {
                    message =  "Api Not Enable"
                });
            }
         await _cameraService.ReconnectCamera(cameraId);
            return new RestDTO<string>()
            {
                Message = "Reconnect CameraSucessfully",
                Data = "",
            };
        }
 
        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<CameraInformationResponse>>>> Get()
        {

            _logger.LogInformation("Access Get Camera");
            IQueryable<CameraInformationResponse> response = await _cameraService.Get();

            return new RestDTO<IQueryable<CameraInformationResponse>>()
            {
                Message = "Get All Camera  Successfully",
                Data = response,
            };
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> Add([FromForm]AddCameraRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            await StorageHandlers.UploadFileAsync(request.CameraImage, request.CameraImage.Name.ToString());
            CameraInformationResponse response = await _cameraService.Add(request);
            return new RestDTO<CameraInformationResponse>()
            {
                Message = "Add Camera Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> Update(Guid id, UpdateCameraRequest request)
        {
            CameraInformationResponse response = await _cameraService.Update(id, request);
            return new RestDTO<CameraInformationResponse>()
            {
                Message = "Delete Location Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> Delete(Guid id)
        {
            CameraInformationResponse response = await _cameraService.Inactive(id);

            return new RestDTO<CameraInformationResponse>()
            {
                Message = "Delete Camera Successfully",
                Data = response,
            };
        }

        //  [Authorize(Roles = Roles.Manager + "," + Roles.User)]


        //? Using in AI
        [ApiKey]
        [HttpPost("{id}/detect")]
        public async Task<ActionResult<RestDTO<DetectResponse>>> DetectFire(Guid id, TakeAlarmRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            DetectResponse response = await _cameraService.DetectFire(id, request,1);
            return new RestDTO<DetectResponse>()
            {
                Message = "Detect Fire  Successfully",
                Data = response,
            };
        }

        [ApiKey]// [Authorize(Roles = Roles.Manager + "," + Roles.User)]
        [HttpPost("{id}/disconnect")]
        public async Task<ActionResult<RestDTO<DetectResponse>>> ElectricalIncident(Guid id)
        {
            await _cameraService.EnableReconnect();
            DetectResponse response = await _cameraService.DetectElectricalIncident(id);
            return new RestDTO<DetectResponse>()
            {
                Message = "Get Alarm Electrical Incident Successfully",
                Data = response,
            };
            //return new RestDTO<DetectResponse>()
            //{
            //    Message = "Get Alarm Electrical Incident Successfully",
            //    Data = null,
            //};
        }

      //  [Authorize(Roles = Roles.Manager + "," + Roles.User)]
        [HttpPost("{id}/alert")]
        public async Task<ActionResult<RestDTO<DetectResponse>>> FireAlarmAlert(Guid id)
        {
            //await StorageHandlers.UploadFileAsync(request.video,"VideoRecord");
            //await StorageHandlers.UploadFileAsync(request.image, "ImageRecord");
            await RealtimeDatabaseHandlers.ModifyMessage(DateTime.UtcNow);
            TakeAlarmRequest takeAlarm = new TakeAlarmRequest
            {
                PictureUrl = "alarmByUserImage.png",
                PredictedPercent = 50,
                VideoUrl = "alarmByUserVideo.mp4"
            };
            DetectResponse response = await _cameraService.DetectFire(id, takeAlarm,3);
            return new RestDTO<DetectResponse>()
            {
                Message = "Detect Fire  Successfully",
                Data = response,
            };
        }


        [Authorize(Roles = Roles.Manager + "," + Roles.User)]
        [HttpPost("{cameraId}/fix")]
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> FixCamera(Guid cameraId)
        {
            var response = await _cameraService.FixCamera(cameraId);
            return new RestDTO<CameraInformationResponse>()
            {
                Message = "Fix Camera  Successfully",
                Data = response,
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestDTO<CameraInformationDetailResponse>>> GetCameraDetail(Guid id)
        {
            return new RestDTO<CameraInformationDetailResponse>()
            {
                Message = "Get Camera Detail Successfully",
                Data = await _cameraService.GetCameraDetail(id),
            };
        }
    }
}
