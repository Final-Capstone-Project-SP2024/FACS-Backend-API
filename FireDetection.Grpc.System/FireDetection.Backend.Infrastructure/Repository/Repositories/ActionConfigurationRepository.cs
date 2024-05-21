using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class ActionConfigurationRepository : IActionConfigurationRepository
    {
        private readonly FireDetectionDbContext _context;
        public ActionConfigurationRepository(FireDetectionDbContext context)
        {
            _context = context;
        }
        public async Task<List<ActionConfigurationResponse>> GetActionConfig()
        {
            return  _context.ActionTypes.OrderBy(x => x.ID).Take(5)
                 .Select(x => new ActionConfigurationResponse { ActionConfigurationId = x.ID, ActionConfigurationDescription = x.ActionDescription, ActionConfigurationName = x.ActionName, Max = x.Max, Min = x.Min }).ToList();

        }

        public async Task UpdateActionConfig(int actionId, UpdateActionConfigRequest request)
        {
            if (request.Max - request.Min < 10) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Distance beetween must in 10");

            if (actionId == 1 && request.Min != 0) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "With actionType == 1, Min value will be 0");

            if (actionId == 5 && request.Max != 100) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "With actionType == 5, Max value will be 100");

            //? just update after with 5
            if (actionId != 1) await UpdateCofigBefore(actionId, (decimal)request.Min);


            //? just update after with 1 
            if(actionId != 5) await UpdateConfigAfter(actionId, (decimal)request.Max);




            var action = await _context.ActionTypes.FirstOrDefaultAsync(x => x.ID == actionId);
            action.Min = (decimal)request.Min;
            action.Max = (decimal)request.Max;
            _context.ActionTypes.Update(action);
            await _context.SaveChangesAsync();

        }


        private async Task UpdateCofigBefore(int actionId, decimal StartPoint)
        {

            var action = _context.ActionTypes.FirstOrDefault(x => x.ID == actionId - 1);
            if (StartPoint - action.Min < 10) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Distance beetween must in 10");
            action.Max = StartPoint;
            _context.ActionTypes.Update(action);
            await _context.SaveChangesAsync();


        }


        private async Task UpdateConfigAfter(int actionId, decimal EndPoint)
        {
            var action = _context.ActionTypes.FirstOrDefault(x => x.ID == actionId + 1);
            if (action.Max - EndPoint < 10) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Distance beetween must in 10");
            action.Min = EndPoint;
            _context.ActionTypes.Update(action);
            await _context.SaveChangesAsync();
        }



    }
}
