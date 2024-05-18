using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
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
           return await _context.ActionTypes.Take(5)
                .Select(x => new ActionConfigurationResponse { ActionConfigurationId = x.ID, ActionConfigurationDescription = x.ActionDescription, ActionConfigurationName = x.ActionName, Max = x.Max, Min = x.Min }).ToListAsync();

        }

        public async Task UpdateActionConfig(int actionId, UpdateActionConfigRequest request)
        {
            var action = await _context.ActionTypes.FirstOrDefaultAsync(x => x.ID == actionId);
            action.Min = (decimal)request.Min;
            action.Max = (decimal)request.Max;
            _context.ActionTypes.Update(action);
            await _context.SaveChangesAsync();

        }
    }
}
