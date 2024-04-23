using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers.Future
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _trasactionService;
        private readonly LinkGenerator _linkGenerator;
        public TransactionController(ITransactionService transactionService, LinkGenerator linkGenerator)
        {
            _trasactionService = transactionService;
            _linkGenerator = linkGenerator;
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{contractId}/renewal")]
        private async Task<ActionResult<RestDTO<TransactionDetailResponse>>> Renewal(Guid contractId, AddTransactionRequest request)
        {
            var response = await _trasactionService.Action(contractId, 3, request);
            return new RestDTO<TransactionDetailResponse>()
            {
                Message = "Vote Record Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{contractId}/upgrade")]
        private async Task<ActionResult<RestDTO<TransactionDetailResponse>>> Upgrade(Guid contractId, AddTransactionRequest request)
        {
            var response = await _trasactionService.Action(contractId, 1, request);
            return new RestDTO<TransactionDetailResponse>()
            {
                Message = "Vote Record Successfully",
                Data = response,
            };
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{contractId}/downgrade")]
        private async Task<ActionResult<RestDTO<TransactionDetailResponse>>> Downgrade(Guid contractId, AddTransactionRequest request)
        {
            var response = await _trasactionService.Action(contractId, 2, request);
            return new RestDTO<TransactionDetailResponse>()
            {
                Message = "Vote Record Successfully",
                Data = response,
            };
        }
        [HttpPost("{transactionId}/paid")]

        private async Task<ActionResult<RestDTO<TransactionDetailResponse>>> Paid(Guid transactionId)
        {
            var response = await _trasactionService.Paid(transactionId);
            return new RestDTO<TransactionDetailResponse>()
            {
                Message = "Set Paid Successfully",
                Data = response,
            };
        }

        [HttpGet("{contractId}")]
        private async Task<ActionResult<RestDTO<IQueryable<TransactionGeneralResponse>>>> GetAll(Guid contractId)
        {
            var response = await _trasactionService.GetTransactions(contractId);
            return new RestDTO<IQueryable<TransactionGeneralResponse>>()
            {
                Message = "Vote Record Successfully",
                Data = response,
            };
        }
    }
}
