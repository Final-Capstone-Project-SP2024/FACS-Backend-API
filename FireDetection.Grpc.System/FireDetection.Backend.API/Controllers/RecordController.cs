using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly LinkGenerator _linkGenerator;

        public RecordController(IRecordService recordService, LinkGenerator linkGenerator)
        {
            _recordService = recordService;
            _linkGenerator = linkGenerator;

        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("{RecordId}/vote")]
        public async Task<ActionResult<RestDTO<VoteAlarmResponse>>> Vote(Guid RecordId, RateAlarmRequest request)
        {
            await _recordService.VoteAlarmLevel(RecordId, request);

            return new RestDTO<VoteAlarmResponse>()
            {
                Message = "Vote Record Successfully",
                Data = null,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Vote),"RecordController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("{RecordId}/action")]
        public async Task<ActionResult<RestDTO<ActionProcessResponse>>> Action(Guid RecordId, AddRecordActionRequest request)
        {
            await _recordService.ActionInAlarm(RecordId, request);
            return new RestDTO<ActionProcessResponse>()
            {
                Message = "Action Successfully",
                Data = null,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Action),"RecordController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<PagedResult<RecordResponse>>>> Get([FromQuery] PagingRequest pagingRequest, [FromQuery] RecordRequest recordRequest)
        {
            var response = await _recordService.Get(pagingRequest, recordRequest);
            return Ok(response);
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet("{recordId}")]
        public async Task<ActionResult<RestDTO<RecordDetailResponse>>> GetDetail(Guid recordId)
        {
            RecordDetailResponse  response = await _recordService.GetDetail(recordId);
            return new RestDTO<RecordDetailResponse>()
            {
                Message = "View Record Detail Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetDetail),"RecordController",
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }

    }
}
