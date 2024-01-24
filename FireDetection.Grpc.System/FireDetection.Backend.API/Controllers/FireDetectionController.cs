using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FireDetectionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> TakeAlarmDetect(TakeAlarmRequest request)
        {
            CloudMessagingHandlers.CloudMessaging("Tap gym","Tap gym xuong 70kg cua nhu");
            return Ok(" hi hi");
        }
    }
}
