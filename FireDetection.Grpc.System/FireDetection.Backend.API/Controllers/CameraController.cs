using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {
        private readonly ICameraService _cameraService;

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<CameInformationResponse>>>> Get()
        {
            IQueryable<CameInformationResponse> response = await _cameraService.Get();

            return new RestDTO<IQueryable<CameInformationResponse>>()
            {
                Message = "Get Cameras Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Get","/CameraController",response, Request.Scheme)!,"self","Get")
                }
            };
        }


        [HttpPost]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Add(AddCameraRequest request)
        {
            CameInformationResponse response = await _cameraService.Add(request);
            return new RestDTO<CameInformationResponse>()
            {
                Message = "Add Cameras Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Add","/CameraController",response, Request.Scheme)!,"self","Post")
                }
            };
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Update(Guid id, AddCameraRequest request)
        {
            CameInformationResponse response = await _cameraService.Update(id, request);
            return new RestDTO<CameInformationResponse>()
            {
                Message = "Add Cameras Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Update","/CameraController",response, Request.Scheme)!,"self","Patch")
                }
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Delete(Guid id)
        {
            CameInformationResponse response = await _cameraService.Inactive(id);

            return new RestDTO<CameInformationResponse>()
            {
                Message = "Delete Cameras Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Delete","/CameraController",response, Request.Scheme)!,"self","Delete")
                }
            };
        }



        [HttpPost("{id}/detect")]
        public async Task<ActionResult<RestDTO<DetectFireResponse>>> DetectFire(Guid id, TakeAlarmRequest request)
        {
            DetectFireResponse detectFire = await _cameraService.DetectFire(id, request);
            return new RestDTO<DetectFireResponse>()
            {
                Message = "Take Fire Detection  Successfully!",
                Data = detectFire,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Take","/CameraController",detectFire, Request.Scheme)!,"self","Delete")
                }
            };
        }


        [HttpPost("{id}/disconnect")]
        public async Task<ActionResult<RestDTO<DetectElectricalIncidentResponse>>> ElectricalIncident(Guid id, TakeElectricalIncidentRequest request)
        {
            DetectElectricalIncidentResponse response = await _cameraService.DetectElectricalIncident(id, request);
            return new RestDTO<DetectElectricalIncidentResponse>()
            {
                Message = "Take Electrical Incident  Detection  Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Take","/CameraController",response, Request.Scheme)!,"self","Delete")
                }
            };
        }


        [HttpPost("{id}}/record")]
        public async Task<ActionResult<RestDTO<DetectElectricalIncidentResponse>>> Record(Guid id, TakeElectricalIncidentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
