﻿using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using GreenDonut;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Location = FireDetection.Backend.Domain.Entity.Location;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly ILocationService _context;

        public LocationController(ILocationService context)
        {
            _context = context;
        }

        [Authorize(Roles = UserRole.Manager)]
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
            };
        }

       // [Authorize(Roles = UserRole.Manager + " " + UserRole.User)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> GetById(Guid id)
        {
            var response = await _context.GetById(id);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "View Location Detail Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> Update(Guid id, AddLocationRequest request)
        {
            LocationInformationResponse response = await _context.UpdateLocation(id, request);

            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Vote Record Successfully",
                Data = response,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> Delete(Guid id)
        {
            await _context.DeleteLocation(id);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Delete Location Successfully",
                Data = null,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet]
        public async Task<ActionResult<RestDTO<IQueryable<LocationGeneralResponse>>>> Get()
        {
            IQueryable<LocationGeneralResponse> location = await _context.GetLocation();

            return new RestDTO<IQueryable<LocationGeneralResponse>>()
            {
                Message = "Get Location Successfully",
                Data = location,
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{id}/addstaff")]
        public async Task<ActionResult<RestDTO<LocationInformationResponse>>> AddStaff(Guid id, AddStaffRequest request)
        {
            var result = await _context.AddStaffToLocation(id, request);
            return new RestDTO<LocationInformationResponse>()
            {
                Message = "Add Staff Successfully",
                Data = result,
            };
        }
    }
}
