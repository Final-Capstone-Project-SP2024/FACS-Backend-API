using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class UserResponsibilityService : IUserResponsibilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserResponsibilityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SaveUserInNotification(Guid recordId, Guid userId)
        {
            if ( await CheckInSystem(recordId, userId))
            {
                UserReponsibility userReponsibility = new UserReponsibility
                {
                    RecordId = recordId,
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow.AddHours(7)
                };
                _unitOfWork.UserResponsibilityRepository.InsertAsync(userReponsibility);
         
                await _unitOfWork.SaveChangeAsync();
            
       
            }
        }

        private  async Task<bool> CheckInSystem(Guid recordId, Guid userId)
        {
            if(await _unitOfWork.UserResponsibilityRepository.Where(x => x.UserId == userId && x.RecordId == recordId).FirstOrDefaultAsync() is not null)
            {
                return false;
            }
            return true;
        }
    }
}
