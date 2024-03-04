using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ITimerService
    {
        public void CheckIsVoting(Guid recordId, string cameraDestination, string cameraLocation);

        public void CheckIsAction(Guid recordId);

        public void SpamNotification(Guid recordId, int alarmLevel);

        public void EndVotePhase(Guid recordId);

    }
}
