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

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
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
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> Add(AddCameraRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            CameraInformationResponse response = await _cameraService.Add(request);
            return new RestDTO<CameraInformationResponse>()
            {
                Message = "Add Camera Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<CameraInformationResponse>>> Update(Guid id, AddCameraRequest request)
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
            DetectResponse response = await _cameraService.DetectFire(id, request);
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
            DetectResponse response = await _cameraService.DetectElectricalIncident(id);
            return new RestDTO<DetectResponse>()
            {
                Message = "Get Alarm Electrical Incident Successfully",
                Data = response,
            };
        }

        [HttpPost("{id}/alert")]
        public async Task<ActionResult<RestDTO<DetectResponse>>> FireAlarmAlert(Guid id,[FromForm] AddAlertByHandResponse request)
        {
            await StorageHandlers.UploadFileAsync(request.video,"VideoRecord");
            await StorageHandlers.UploadFileAsync(request.image, "ImageRecord");
            TakeAlarmRequest takeAlarm = new TakeAlarmRequest
            {
                PictureUrl = request.image.ToString(),
                PredictedPercent = request.FireDetection,
                VideoUrl = request.video.ToString()
            };
            DetectResponse response = await _cameraService.DetectFire(id, takeAlarm);
            return new RestDTO<DetectResponse>()
            {
                Message = "Detect Fire  Successfully",
                Data = response,
            };
        }
    }
}
