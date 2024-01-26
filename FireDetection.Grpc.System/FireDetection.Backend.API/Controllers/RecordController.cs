using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static Google.Apis.Requests.BatchRequest;

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

        [HttpPost("{RecordId}/action")]
        public async Task<ActionResult<RestDTO<ActionProcessResponse>>> Action(Guid RecordID, AddRecordActionRequest request)
        {
            await _recordService.ActionInAlarm(RecordID, request);
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
      
    }
}
