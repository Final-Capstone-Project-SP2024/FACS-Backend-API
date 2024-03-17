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
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public FeedbackService(IUnitOfWork unitOfWork, IClaimsService claimsService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _mapper = mapper;

        }
        public async Task<FeedbackResponse> Feedback(AddFeedbackRequest request)
        {
            Feedback feedback = _mapper.Map<Feedback>(request);
            _unitOfWork.FeedbackRepository.InsertAsync(feedback);
            feedback.UserId = _claimsService.GetCurrentUserId;
            await _unitOfWork.SaveChangeAsync();


            return await GetByContext(request.Context);
        }

        public async Task<IQueryable<FeedbackResponse>> Get()
        {
            var data = await _unitOfWork.FeedbackRepository.GetAll(); // Assuming GetAllAsync() exists
            var mappedData = data.Select(f => _mapper.Map<FeedbackResponse>(f));
            return mappedData.AsQueryable();
        }

        internal  async Task<FeedbackResponse> GetByContext(string Context)
        {
            var contexts =  _unitOfWork.FeedbackRepository.Where(x => x.Context == Context).FirstOrDefault();

            return _mapper.Map<FeedbackResponse>(contexts);

        }
    }
}
