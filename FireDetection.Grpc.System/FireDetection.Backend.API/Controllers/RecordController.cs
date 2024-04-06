using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;


namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    public class RecordController : BaseController
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;

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
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{RecordId}/action")]
        public async Task<ActionResult<RestDTO<ActionProcessResponse>>> Action(Guid RecordId, AddRecordActionRequest request)
        {
            await _recordService.ActionInAlarm(RecordId, request);
            return new RestDTO<ActionProcessResponse>()
            {
                Message = "Action Successfully",
                Data = null,
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
            RecordDetailResponse response = await _recordService.GetDetail(recordId);
            return new RestDTO<RecordDetailResponse>()
            {
                Message = "View Record Detail Successfully",
                Data = response,
            };
        }



        [HttpPost("{Id}/endvote")]
        public async Task<ActionResult<RestDTO<RecordDetailResponse>>> EndVote(Guid Id)
        {
            await _recordService.EndVotePhase(Id);
            RecordDetailResponse response = await _recordService.GetDetail(Id);
            return new RestDTO<RecordDetailResponse>()
            {
                Message = "End Vote Record  Successfully",
                Data = response,
            };
        }


        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("{recordId}/addEvidence")]
        public async Task<ActionResult<RestDTO<RecordDetailResponse>>> AddNewEvident(Guid recordId,IFormFile evidentAdding)
        {
            await _recordService.AddEvidence(evidentAdding, recordId);
            return new RestDTO<RecordDetailResponse>()
            {
                Message = "End Vote Record  Successfully",
                Data = null,
            };
        }
    }

}

