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
        private readonly IMemoryCacheService _memorycachedservice;
        public TimerService(IUnitOfWork unitOfWork,IMemoryCacheService memoryCacheService)
        {
            _unitOfWork = unitOfWork;
            _memorycachedservice = memoryCacheService;


        }
        public void CheckIsVoting(Guid recordId)
        {
            Task.Run(async () => await CheckAndSendNotification(recordId));
        }



        private async Task CheckAndSendNotification(Guid recorid)
        {
            bool   check  = true;
            while (check)
            {
                try
                {
                    if (!await _memorycachedservice.CheckRecordKeyIsVote(recorid))
                    {
                        // Data not found, send notification
                  //     await CloudMessagingHandlers.CloudMessaging();
                        Console.WriteLine("=====================");
                    }
                    else
                    {
                       check = false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    Console.WriteLine($"Error: {ex.Message}");
                }

                await Task.Delay(5000);
            }
            
        }

     


    }
}
