using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IAPICallService _APICallService;

        private readonly IMemoryCacheService _memorycachedservice;



        public TimerService(IUnitOfWork unitOfWork, IMemoryCacheService memoryCacheService, IAPICallService aPICallService)
        {
            _unitOfWork = unitOfWork;
            _memorycachedservice = memoryCacheService;
            _APICallService = aPICallService;
        }

        public void CheckIsAction(Guid recordId)
        {
            Task.Run(async () => await CheckAndAutoAction(recordId));
        }

        public void CheckIsVoting(Guid recordId)
        {
            Task.Run(async () => await CheckAndSendNotification(recordId));
        }


        private async Task CheckAndSendNotification(Guid recorid)
        {
            bool check = true;
            while (check)
            {
                try
                {
                    if (!await _memorycachedservice.CheckRecordKeyIsVote(recorid))
                    {
                        var response = await NotificationHandler.Get(6);
                        // Data not found, send notification
                        await CloudMessagingHandlers.CloudMessaging(titleInput: response.Title, bodyInput: response.Context);
                        Console.WriteLine("========Voting Phase=====");
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
                if (!check)
                {
                    await Task.Delay(5000);
                    Record record = _unitOfWork.RecordRepository.Where(x => x.Id == recorid).FirstOrDefault();
                    record.UserRatingPercent = await _memorycachedservice.VotingResult();
                    _unitOfWork.RecordRepository.Update(record);
                    await _unitOfWork.SaveChangeAsync();
                }
                Console.WriteLine("End Task");
            }

        }

        private async Task CheckAndAutoAction(Guid recordId)
        {
            bool check = true;
            DateTime startTime = DateTime.Now;
            while (check)
            {
                try
                {
                    if (!await _memorycachedservice.CheckIsAction(recordId))
                    {
                        //! Send notification to remind manager have some action
                         await CloudMessagingHandlers.CloudMessaging();
                        Console.WriteLine("======Action Phase=======");
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

                Console.WriteLine("End Task");

                if ((DateTime.Now - startTime).TotalMinutes >= 0.2)
                {
                    Console.WriteLine("Performing action after 5 minutes...");
                    await _APICallService.AutoCallAction(recordId, await _memorycachedservice.VotingResult());
                    break; // Exit the loop after performing the action
                }
            }
        }




    }
}
