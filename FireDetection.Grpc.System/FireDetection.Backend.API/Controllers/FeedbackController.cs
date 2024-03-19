using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
using static Google.Apis.Requests.BatchRequest;

namespace FireDetection.Backend.API.Controllers
{
    
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _feedbackService;
        private readonly LinkGenerator _linkGenerator;

        public FeedbackController(IFeedbackService feedbackService, LinkGenerator linkGenerator)
        {
            _feedbackService = feedbackService;
            _linkGenerator = linkGenerator;
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        private async Task<ActionResult<RestDTO<FeedbackResponse>>> Feedback(AddFeedbackRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            var response = await _feedbackService.Feedback(request);
            return new RestDTO<FeedbackResponse>()
            {
                Message = "Feedback Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Feedback),"FeedbackController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        private async Task<ActionResult<RestDTO<IQueryable<FeedbackResponse>>>> GetAll()
        {
            var response = await _feedbackService.Get();
            return new RestDTO<IQueryable<FeedbackResponse>>()
            {
                Message = "Feedback Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(GetAll),"FeedbackController",""
                        ,
                        Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }
    }
}
