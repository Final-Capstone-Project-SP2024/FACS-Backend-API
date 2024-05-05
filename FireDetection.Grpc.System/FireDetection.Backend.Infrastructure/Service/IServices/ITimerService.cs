using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ITimerService
    {
        public void CheckIsVoting(Guid recordId, string cameraDestination, string cameraLocation,List<Guid> users);

        public void CheckIsAction(Guid recordId,List<Guid> users);

        public void SpamNotification(Guid recordId, int alarmLevel,List<Guid> users,string cameraDestination,string locationName);

        public void EndVotePhase(Guid recordId);

        public void DisconnectionNotification(Guid recordId, string cameraDestination, string cameraLocation);


    }
}
