using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IFeedbackService
    {
        public Task<FeedbackResponse> Feedback(AddFeedbackRequest request);

        public Task<IQueryable<FeedbackResponse>> Get();
    }
}
