using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IAPICallService
    {
        public Task AutoCallAction(Guid recordId, int actionId);
        
        public Task AutoCompleteVoting(Guid recordId);
    }
}
