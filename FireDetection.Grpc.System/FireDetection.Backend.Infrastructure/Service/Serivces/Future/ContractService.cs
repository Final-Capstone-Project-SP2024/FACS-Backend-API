using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces.Future
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ContractDetailResponse> Add(AddContractRequest request)
        {
            var newContract = _mapper.Map<Contract>(request);

            _unitOfWork.ContractRepository.InsertAsync(newContract);
            await _unitOfWork.SaveChangeAsync();

            return await GetByContractImage(request.ContractImage);
        }

        public async Task<IQueryable<ContractGeneralResponse>> GetAll()
        {
            var data = await _unitOfWork.ContractRepository.GetAll();
            var mappedData = data.Select(x => _mapper.Map<ContractGeneralResponse>(x));
            return mappedData.AsQueryable();
        }

        public async Task<ContractDetailResponse> GetDetail(Guid contractId)
        {
            return _mapper.Map<ContractDetailResponse>(_unitOfWork.ContractRepository.GetById(contractId));
        }

        public async Task<ContractDetailResponse> Subcribe(Guid contractId)
        {
            Contract contract = await _unitOfWork.ContractRepository.GetById(contractId);
            contract.isActive = true;

            await Update(contract);

            return await GetByContractImage(contract.ContractImage);
        }

        public async Task<ContractDetailResponse> Unsubcribe(Guid contractId)
        {
            Contract contract = await _unitOfWork.ContractRepository.GetById(contractId);
            contract.isActive = true;

            await Update(contract);

            return await GetByContractImage(contract.ContractImage);
        }

        public async Task<ContractDetailResponse> Update(Guid contractId, UpdateContractRequest request)
        {
            var contract = await _unitOfWork.ContractRepository.GetById(contractId);
            _mapper.Map(contract, request);

            await Update(contract);
            return await GetByContractImage(contract.ContractImage);
        }

        internal async Task<ContractDetailResponse> GetByContractImage(string ContractImage)
        {
            var contracts = _unitOfWork.ContractRepository.Where(x => x.ContractImage == ContractImage).FirstOrDefault();
            return _mapper.Map<ContractDetailResponse>(contracts);

        }


        internal async Task Update(Contract contract)
        {
            _unitOfWork.ContractRepository.Update(contract);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
