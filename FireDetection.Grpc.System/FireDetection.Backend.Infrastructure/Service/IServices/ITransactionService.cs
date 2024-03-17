using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ITransactionService
    {
        public Task<TransactionDetailResponse> Action(Guid contractId, int type,AddTransactionRequest request);

        public Task<IQueryable<TransactionGeneralResponse>> GetTransactions(Guid contractId);
        public Task<TransactionDetailResponse> Paid(Guid contractId); 

    }
}
