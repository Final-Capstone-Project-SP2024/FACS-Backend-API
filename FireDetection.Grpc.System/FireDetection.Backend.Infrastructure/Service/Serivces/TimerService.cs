using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.DAL;
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

        private readonly IMemoryCacheService _memorycachedservice;



        public TimerService(IUnitOfWork unitOfWork, IMemoryCacheService memoryCacheService)
        {
            _unitOfWork = unitOfWork;
            _memorycachedservice = memoryCacheService;


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
                        // Data not found, send notification
                        //     await CloudMessagingHandlers.CloudMessaging();
                        Console.WriteLine("=====================_____Vote");
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
                        // Data not found, send notification
                        //     await CloudMessagingHandlers.CloudMessaging();
                        Console.WriteLine("=====================-----Action");
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

                    try
                    {

                        APICal call = new APICal(_unitOfWork);
                      await  call.AutoCallAction(recordId,  1);

                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                        throw;
                    }
                    break; // Exit the loop after performing the action
                }
            }
        }




    }
}
