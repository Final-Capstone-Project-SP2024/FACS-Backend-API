using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
