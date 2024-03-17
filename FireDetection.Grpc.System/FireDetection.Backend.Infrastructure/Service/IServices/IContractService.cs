using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IContractService
    {
        public  Task<ContractDetailResponse> Add(AddContractRequest request);

        public Task<IQueryable<ContractGeneralResponse>> GetAll();

        public Task<ContractDetailResponse> GetDetail(Guid contractId);

        public Task<ContractDetailResponse> Update(Guid contractId, UpdateContractRequest request);

        public Task<ContractDetailResponse> Subcribe(Guid contractId);

        public Task<ContractDetailResponse> Unsubcribe(Guid contractId);
    }
}
