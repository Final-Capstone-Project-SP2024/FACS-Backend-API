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

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public TransactionService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _mapper = mapper;

        }
        public async Task<TransactionDetailResponse> Action(Guid contractId, int type, AddTransactionRequest request)
        {
             if(type == 3)
            {
              return  await Renewal(contractId, request);
            }
            return null;
        }

        public Task<IQueryable<TransactionGeneralResponse>> GetTransactions(Guid contractId)
        {
            throw new NotImplementedException();
        }


        internal async Task<TransactionDetailResponse> Renewal(Guid contractId, AddTransactionRequest request)
        {

            DateTime dateTime = DateTime.UtcNow;
            Transaction transaction = new Transaction()
            {
                ActionPlanTypeID = 3,
                ContractID = contractId,
                isPaid = request.IsPaid,
                PaymentTypeID = request.PaymentType,
                Price = 100,
                UserID = _claimsService.GetCurrentUserId,
                IsDeleted = false,
                CreatedDate = dateTime,
            };
            _unitOfWork.TransactionRepository.InsertAsync(transaction);
            await _unitOfWork.SaveChangeAsync();
            return await GetByDateTime(dateTime);
        }


        internal async Task<TransactionDetailResponse> GetByDateTime(DateTime dateTime)
        {
            return _mapper.Map<TransactionDetailResponse>(_unitOfWork.TransactionRepository.Where(x => x.CreatedDate == dateTime).FirstOrDefault());
        }

        public async Task<TransactionDetailResponse> Paid(Guid trasanctionID)
        {
            Transaction transaction = await _unitOfWork.TransactionRepository.GetById(trasanctionID);
            if (transaction == null)
            {
                throw new Exception();
            }
            transaction.isPaid = true;
           _unitOfWork.TransactionRepository.Update(transaction);
            await _unitOfWork.SaveChangeAsync();

            return await GetByDateTime(transaction.CreatedDate);
        }
    }
}
