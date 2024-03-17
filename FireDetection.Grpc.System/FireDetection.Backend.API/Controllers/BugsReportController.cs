using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    public class BugsReportController : BaseController
    {
        private readonly IBugsReportService _bugsReportService;
        private readonly LinkGenerator _linkGenerator;

        public BugsReportController(IBugsReportService bugsReportService, LinkGenerator linkGenerator)
        {
            _bugsReportService = bugsReportService;
            _linkGenerator = linkGenerator;
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost]
        public async Task<ActionResult<RestDTO<BugsReportResponse>>> Add(BugsReportRequest request)
        {
            var response = await _bugsReportService.Add(request);
            return new RestDTO<BugsReportResponse>()
            {
                Message = "Add Bug Report Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"BugsReportController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }




        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<BugsReportResponse>>>> Get()
        {
            var response = await _bugsReportService.GetAll();
            return new RestDTO<IQueryable<BugsReportResponse>>()
            {
                Message = "Get Bug Report Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Get),"BugsReportController",
                        "",
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }

        [HttpPost("{Id}")]
        public async Task<ActionResult<RestDTO<BugsReportResponse>>> Solve(Guid Id)
        {
           var response = await _bugsReportService.Solve(Id);
            return new RestDTO<BugsReportResponse>()
            {
                Message = "Solve Bug Report Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Feedback),"BugsReportController",
                        "",
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }
    }
}
