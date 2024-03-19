using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    public class ManualPlanController : BaseController
    {
        private readonly IManualPlanService _manualPlanService;
        private readonly LinkGenerator _linkGenerator;
        public ManualPlanController(IManualPlanService manualPlanService, LinkGenerator linkGenerator)
        {
           _manualPlanService = manualPlanService;
            _linkGenerator = linkGenerator;

        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<ManualPlanGeneralResponse>>>> GetAll()
        {
            var response = await _manualPlanService.GetAll();
            return new RestDTO<IQueryable<ManualPlanGeneralResponse>>()
            {
                Message = "Get All ManualPlan Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetAll),"ManualPlanController",
                        "",
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }
        [Authorize(Roles = UserRole.Manager)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<RestDTO<ManualPlanDetailResponse>>> GetDetail(int Id)
        {
            var response = await _manualPlanService.GetManualPlanDetail(Id);
            return new RestDTO<ManualPlanDetailResponse>()
            {
                Message = "Get All ManualPlan Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetDetail),"ManualPlanController",
                        Id,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{Id}")]
        public async Task<ActionResult<RestDTO<ManualPlanDetailResponse>>> Update(int Id,UpdateManualPlanRequest request)
        {
            if(!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            var response = await _manualPlanService.Update(Id, request);
            return new RestDTO<ManualPlanDetailResponse>()
            {
                Message = "Update ManualPlan Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Update),"ManualPlanController",
                        Id,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }
    }
}
