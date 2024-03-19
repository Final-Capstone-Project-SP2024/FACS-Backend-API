using AutoMapper;
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

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<CameraInformationResponse>>>> Get()
        {

            _logger.LogInformation("Access Get Camera");
            IQueryable<CameraInformationResponse> response = await _cameraService.Get();

            return new RestDTO<IQueryable<CameraInformationResponse>>()
            {
                Message = "Get All Camera  Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Get),"/CameraController",Request.Scheme))!,
                    "self",
                    "Get")
                }
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
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"CameraController",Request.Scheme))!,
                    "self",
                    "Add")
                }
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
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Update),"Location",Request.Scheme))!,
                    "self",
                    "Patch")
                }
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
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Delete),"Location",Request.Scheme))!,
                    "self",
                    "Delete")
                }
            };
        }

      //  [Authorize(Roles = Roles.Manager + "," + Roles.User)]
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
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(DetectFire),"CameraController",Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

       // [Authorize(Roles = Roles.Manager + "," + Roles.User)]
        [HttpPost("{id}/disconnect")]
        public async Task<ActionResult<RestDTO<DetectResponse>>> ElectricalIncident(Guid id)
        {
            DetectResponse response = await _cameraService.DetectElectricalIncident(id);
            return new RestDTO<DetectResponse>()
            {
                Message = "Get Alarm Electrical Incident Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(ElectricalIncident),"CameraController",Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

        //[HttpPost("{id}/record")]
        //public async Task<ActionResult<RestDTO<DetectResponse>>> Record(Guid id, TakeElectricalIncidentRequest request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
