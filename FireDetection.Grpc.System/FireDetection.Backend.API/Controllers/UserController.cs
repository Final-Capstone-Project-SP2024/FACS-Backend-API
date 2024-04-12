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
using System.Security;
using static Google.Apis.Requests.BatchRequest;
namespace FireDetection.Backend.API.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
            };
        }

        [Authorize(Roles =  UserRole.Manager)]
        [HttpPost("{id}/active")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Active(Guid id)
        {
            await _userService.ActiveUser(id);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Active Account Successfully!",
                Data = null,
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
            };
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPatch]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> Update(UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }

            UserInformationResponse response = await _userService.UpdateUser(request);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Update Successfully!",
                Data = response,
            };
        }



        [HttpGet]
        public async Task<ActionResult<RestDTO<List<UserInformationResponse>>>> GetAllUsers([FromQuery] PagingRequest pagingRequest, [FromQuery] UserRequest request)
        {
            var response = await _userService.GetAllUsers(pagingRequest, request);
            return response != null ? Ok(new
            {
                Message = "Get All Users Successfully!",
                Data = response,
            }) : NotFound();
        }


        [Authorize(Roles = UserRole.Manager)]
        [HttpPost("sendAccount")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> SendMail(string emai)
        {
            await _userService.SendEmail(emai);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Send Account Successfully!",
                Data = null,
            };
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<RestDTO<UserInformationDetailResponse>>> GetDetail(Guid userId)
        {

            var response = await _userService.GetDetail(userId);
            return new RestDTO<UserInformationDetailResponse>()
            {
                Message = "Get  Account Detail Successfully!",
                Data = response,

            };
        }



        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("forgetpassword")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> ForgetPassword(string securityCode)
        {
            await _userService.ForgotPassword(securityCode);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Check your mail and change the password",
                Data = null,
            };
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("otpconfirm")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> ConfirmOTP(ChangePasswordRequest request)
        {
            bool check = await _userService.ChangePassword(request);
            if (check == true)
            {
                return new RestDTO<UserInformationResponse>()
                {
                    Message = "Change password successfulyy!",
                    Data = null,
                };
            }
            else
            {
                return new RestDTO<UserInformationResponse>()
                {
                    Message = "Change password unsuccessfulyy!",
                    Data = null,
                };
            }

        }


        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost("changepassword")]
        public async Task<ActionResult<RestDTO<UserInformationResponse>>> ChangePassword(ChangePasswordByUserRequest request)
        {
            var response = await _userService.ChangePasswordByUser(request);
            return new RestDTO<UserInformationResponse>()
            {
                Message = "Change password successfulyy!",
                Data = response,
            };

        }

        [HttpGet("unregistered")]
        public async Task<ActionResult<RestDTO<IEnumerable<UserInformationResponse>>>> UnRegister(){

            return new RestDTO<IEnumerable<UserInformationResponse>>()
            {
                Message = "Get list unregistered User to Location successfully!",
                Data = await _userService.UnRegisterLocaiton(),
            };
        }

    }
}
