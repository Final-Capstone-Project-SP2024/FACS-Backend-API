using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Google.Apis.Requests.BatchRequest;
namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly LinkGenerator _linkGenerator;
        public UserController(IUserService userService, LinkGenerator linkGenerator)
        {
            _userService = userService;
            _linkGenerator = linkGenerator;
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Add(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            UserInformationResponse response = await _userService.CreateUser(request);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Create User Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"/UserController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }
    
        
        [HttpPost("login")]
        public async Task<ActionResult<RestDTO<UserLoginResponse>>> Login(UserLoginRequest req)
        {
            UserLoginResponse response = await _userService.Login(req);
            return new RestDTO<UserLoginResponse>()
            {
                Message = "Login Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(Login),"UserController",req, Request.Scheme))!,"self","Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{id}/active")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Active(Guid id)
        {
            await _userService.ActiveUser(id);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Active Account Successfully!",
                Data = null,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(Active), "UserController", null, Request.Scheme))!, "self", "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("{id}/inactive")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Inactive(Guid id)
        {
            await _userService.InactiveUser(id);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Inactive Account Successfully!",
                Data = null,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(Inactive), "UserController", null, Request.Scheme))!, "self", "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpPatch("{id}")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Update(Guid id, UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }

            UserInformationResponse response = await _userService.UpdateUser(id, request);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Update Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("Update","UserController",response, Request.Scheme)!,"self","Post")
                }
            };
        }



        [Authorize(Roles = UserRole.Manager )]
        [HttpGet]
        public async Task<ActionResult<RestDTO<List<UserInformationResponse>>>> GetAllUsers([FromQuery] PagingRequest pagingRequest, [FromQuery] UserRequest request)
        {
            var response = await _userService.GetAllUsers(pagingRequest, request);
            return response != null ? Ok(new
            {
                Message = "Get All Users Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(GetAllUsers), "UserController", null, Request.Scheme))!,"self","Get")
                }
            }) : NotFound();
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("sendAccount")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> SendMail(string emai)
        {
            await _userService.SendEmail(emai);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Inactive Account Successfully!",
                Data = null,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(SendMail), "UserController", null, Request.Scheme))!, "self", "Post")
                }
            };
        }

        [Authorize(Roles = UserRole.Manager)]
        [HttpGet("userId")]
        public async Task<ActionResult<RestDTO<UserInformationDetailResponse>>> GetDetail(Guid userId)
        {

            var response = await _userService.GetDetail(userId);
            return new RestDTO<UserInformationDetailResponse>()
            {
                Message = "Get  Account Detail Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(GetDetail), "UserController", userId, Request.Scheme))!, "self", "Get")
                }
            };
        }
     


    
    }
}
