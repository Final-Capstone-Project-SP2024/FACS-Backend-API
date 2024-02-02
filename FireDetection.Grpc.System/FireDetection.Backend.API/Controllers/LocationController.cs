using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _context;
        private readonly LinkGenerator _linkGenerator;

        public LocationController(ILocationService context, LinkGenerator linkGenerator)
        {
            _context = context;
            _linkGenerator = linkGenerator;
        }


        [HttpPost]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> Add(AddLocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            LocationInformationResponse response = await _context.AddNewLocation(request);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Add Location Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"/LocationController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };


        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> Update(Guid id, AddLocationRequest request)
        {
            LocationInformationResponse response = await _context.UpdateLocation(id, request);

            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Vote Record Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Update),"/LocationController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Patch")
                }
            };
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> Delete(Guid id)
        {
            await _context.DeleteLocation(id);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Delete Location Successfully",
                Data = null,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Delete),"RecordController",Request.Scheme))!,
                    "self",
                    "Delete")
                }
            };
        }

        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<Domain.Entity.Location>>>> Get()
        {
            IQueryable<Domain.Entity.Location> location = await _context.GetLocation();

            return new RestDTO<IQueryable<Domain.Entity.Location>>()
            {
                Message = "Delete Location Successfully",
                Data = location,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Get),"RecordController",Request.Scheme))!,
                    "self",
                    "Get")
                }
            };
        }

        [HttpPost("/{id}/add")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> AddStaff(Guid id, AddStaffRequest request)
        {
            var result = await _context.AddStaffToLocation(id, request);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Add Staff Successfully",
                Data = result,
                Links = new List<LinkDTO> {
                  new LinkDTO(
                    Url.Action(
                     _linkGenerator.GetUriByAction(HttpContext,nameof(AddStaff),"/LocationController",
                      request,
                      Request.Scheme))!,
                "self",
                 "Post")
    }
            };
        }
    }
}
