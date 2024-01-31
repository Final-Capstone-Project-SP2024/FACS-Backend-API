using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;



namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class TimerService : ITimerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Timer _timer;
        public TimerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public void CheckIsVoting(Guid recordId)
        {
            _timer = new Timer(async _ => await CheckAndSendNotification(recordId), null, 0, 5000);
        }



        private async Task CheckAndSendNotification(Guid recorid)
        {
            try
            {
                if (await Checked(recorid))
                {
                    // Data not found, send notification
                    await CloudMessagingHandlers.CloudMessaging();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task<bool> Checked(Guid recordID)
        {
            var recordProcesses = await _unitOfWork.RecordProcessRepository.GetAll();
            bool conditionMet = recordProcesses.Any(x => x.RecordID == recordID);

            // Additional logic based on your actual condition
            return conditionMet;
        }


    }
}
