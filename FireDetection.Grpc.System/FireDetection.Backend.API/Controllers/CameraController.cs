using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;

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
           IQueryable<CameInformationResponse> response = await  _cameraService.Get();

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
            CameInformationResponse response = await  _cameraService.Add(request);
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
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Update(Guid id,AddCameraRequest request)
        {
           CameInformationResponse response = await _cameraService.Update(id,request);
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
          CameInformationResponse response =  await _cameraService.Inactive(id);

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
    }
}
