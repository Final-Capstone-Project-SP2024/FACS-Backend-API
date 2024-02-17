using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql.Internal.TypeHandlers;
using System.Diagnostics;
using System.Linq.Dynamic.Core;

namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LinkGenerator _linkGenerator;
        public UserController(IUserService userService, LinkGenerator linkGenerator)
        {
            _userService = userService;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Add(CreateUserRequest request)
        {
            UserInformationResponse response = await _userService.CreateUser(request);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Create User Successfully",
                Data = response,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                    Url.Action(
                        _linkGenerator.GetUriByAction(HttpContext,nameof(Add),"UserController",
                        request,
                        Request.Scheme))!,
                    "self",
                    "Post")
                }
            };
        }
        [HttpPost("/login")]
        public async Task<ActionResult<RestDTO<UserLoginResponse>>> Login(UserLoginRequest req)
        {
            UserLoginResponse response = await _userService.Login(req);
            return new RestDTO<UserLoginResponse>()
            {
                Message = "Login Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("login","UserController",req, Request.Scheme)!,"self","Post")
                }
            };
        }


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
                    new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(Active), "UserController", null, Request.Scheme))  !, "self", "Post")
                }
            };
        }


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

        [HttpGet("/{id}")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }


        [HttpPatch("/{id}")]
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
                Message = "Login Successfully!",
                Data = response,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action("login","UserController",response, Request.Scheme)!,"self","Post")
                }
            };
        }

        //[HttpGet]
        //public async Task<RestDTO<PagedResult<UserInformationResponse>>> GetAllUsers(PagingRequest pagingRequest, UserRequest request){
        //    var response = await _userService.GetAllUsers(pagingRequest, request);
        //    return response != null ? new RestDTO<PageResult<UserInformationResponse>>()
        //    {
        //        Message = "Get All Successfully!",
        //        Data = response,
        //        Links = new List<LinkDTO>
        //        {
        //            new LinkDTO(Url.Action(_linkGenerator.GetUriByAction(HttpContext,nameof(GetAllUsers), "UserController", null, Request.Scheme))!,"self","Get")
        //        }
        //    } : NotFound();
        //}
    }
}
