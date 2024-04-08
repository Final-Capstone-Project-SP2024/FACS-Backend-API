using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FireDetection.Backend.API.Controllers
{
    public class TokenController : BaseController
    {
        private readonly IClaimsService _claimService;
        public TokenController(IClaimsService claimsService)
        {
            _claimService = claimsService;
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpPost]
        public async Task<ActionResult> AddToken(string token)
        {
            await RealtimeDatabaseHandlers.AddFCMToken((Guid)_claimService.GetCurrentUserId, token);
            return Ok("Add Successfully");
        }

        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet]
        public async Task<ActionResult> GetToken()
        {
                return Ok(await RealtimeDatabaseHandlers.GetFCMToken(_claimService.GetCurrentUserId));

        }
        [Authorize(Roles = UserRole.Manager + "," + UserRole.User)]
        [HttpGet("/all")]
        public async Task<ActionResult> GetTokens()
        {
            Dictionary<string, string> data =  JsonConvert.DeserializeObject<Dictionary<string, string>>(await RealtimeDatabaseHandlers.GetFCMToken());
            List<string> values = new List<string>(data.Values);

            // Print the values
            foreach (var value in values)
            {
                Console.WriteLine(value);
            }
            return Ok(await RealtimeDatabaseHandlers.GetFCMToken());

        }
    }
}
