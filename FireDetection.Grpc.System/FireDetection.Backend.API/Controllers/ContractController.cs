using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    public class ContractController : BaseController
    {
        private readonly IContractService _contractService;
        private readonly LinkGenerator _linkGenerator;
        public ContractController(IContractService contractService, LinkGenerator linkGenerator)
        {
            _contractService = contractService;
            _linkGenerator = linkGenerator;
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public async Task<ActionResult<RestDTO<ContractDetailResponse>>> Add(AddContractRequest request)
        {
            var response = await _contractService.Add(request);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Create Contract Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"/ContractController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }


        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<ContractGeneralResponse>>>> GetAll()
        {
            var response = await _contractService.GetAll();
            return new RestDTO<IQueryable<ContractGeneralResponse>>()
            {
                Message = "Get Contracts Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetAll),"/ContractController",
                        "",
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }

        [HttpGet("{contractId}")]
        public async Task<ActionResult<RestDTO<ContractDetailResponse>>> Get(Guid contractId)
        {
            var response = await _contractService.GetDetail(contractId);    
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Get Contract Detail Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Get),"/ContractController",
                        contractId,
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }


        [HttpPatch("{contractId}")]
        public async Task<ActionResult<RestDTO<ContractDetailResponse>>> Update(Guid contractId, UpdateContractRequest request)
        {
            var response = await _contractService.Update(contractId, request);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Update Contract  Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Update),"/ContractController",
                        contractId,
                        Request.Scheme))!,
                    "self",
                    "Patch")
                }
            };
        }


        [HttpPost("{contractId}/subcribe")]
        public async Task<ActionResult<RestDTO<ContractDetailResponse>>> Subcribe(Guid contractId)
        {
            var response = await _contractService.Subcribe(contractId);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Subcribe Contract  Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Subcribe),"/ContractController",
                        contractId,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }


        [HttpPost("{contractId}/unsubcribe")]
        public async Task<ActionResult<RestDTO<ContractDetailResponse>>> Unsubcribe(Guid contractId)
        {
            var response = await _contractService.Unsubcribe(contractId);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Unsubcribe Contract  Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Unsubcribe),"/ContractController",
                        contractId,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

    }
}
