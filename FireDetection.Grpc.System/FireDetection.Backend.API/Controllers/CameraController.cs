using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Get()
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Add(AddCameraRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Update(Guid id,AddCameraRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RestDTO<CameInformationResponse>>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }    
    }
}
