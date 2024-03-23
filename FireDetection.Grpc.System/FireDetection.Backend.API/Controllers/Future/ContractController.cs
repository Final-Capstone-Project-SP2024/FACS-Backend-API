using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers.Future
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
        [Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        private async Task<ActionResult<RestDTO<ContractDetailResponse>>> Add(AddContractRequest request)
        {
            var response = await _contractService.Add(request);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Create Contract Successfully",
                Data = response,
            };
        }



        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        private async Task<ActionResult<RestDTO<IQueryable<ContractGeneralResponse>>>> GetAll()
        {
            var response = await _contractService.GetAll();
            return new RestDTO<IQueryable<ContractGeneralResponse>>()
            {
                Message = "Get Contracts Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet("{contractId}")]
        private async Task<ActionResult<RestDTO<ContractDetailResponse>>> Get(Guid contractId)
        {
            var response = await _contractService.GetDetail(contractId);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Get Contract Detail Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]

        [HttpPatch("{contractId}")]
        private async Task<ActionResult<RestDTO<ContractDetailResponse>>> Update(Guid contractId, UpdateContractRequest request)
        {
            var response = await _contractService.Update(contractId, request);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Update Contract  Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]

        [HttpPost("{contractId}/subcribe")]
        private async Task<ActionResult<RestDTO<ContractDetailResponse>>> Subcribe(Guid contractId)
        {
            var response = await _contractService.Subcribe(contractId);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Subcribe Contract  Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{contractId}/unsubcribe")]
        private async Task<ActionResult<RestDTO<ContractDetailResponse>>> Unsubcribe(Guid contractId)
        {
            var response = await _contractService.Unsubcribe(contractId);
            return new RestDTO<ContractDetailResponse>()
            {
                Message = "Unsubcribe Contract  Successfully",
                Data = response,
            };
        }

    }
}
