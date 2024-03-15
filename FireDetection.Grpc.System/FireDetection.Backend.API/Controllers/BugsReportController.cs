using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace FireDetection.Backend.API.Controllers
{
    public class BugsReportController : BaseController
    {

        [HttpPost]
        public async Task<ActionResult<RestDTO<BugsReportResponse>>> Add(BugsReportRequest request)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public async Task<ActionResult<RestDTO<List<BugsReportResponse>>>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
