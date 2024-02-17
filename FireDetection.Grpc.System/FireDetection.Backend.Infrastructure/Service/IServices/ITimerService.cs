using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ITimerService
    {
        public void CheckIsVoting(Guid recordId);

        public void CheckIsAction(Guid recordId);
      

    }
}
