using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Utils;
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

        private readonly IMemoryCacheService _memorycachedservice;
        private readonly IAPICallService _apiCall;
        private CancellationTokenSource _cancellationTokenSource;
        public TimerService(IMemoryCacheService memoryCacheService, IAPICallService aPICall)
        {
            _memorycachedservice = memoryCacheService;
            _apiCall = aPICall;
        }


        public void EndVotePhase(Guid recordId)
        {
            Task.Run(async () => await EndVotingPhase(recordId));
        }
        public void CheckIsAction(Guid recordId)
        {
            Task.Run(async () => await CheckAndAutoAction(recordId));
        }

        public void CheckIsVoting(Guid recordId, string cameraDestination, string cameraLocation)
        {
            Task.Run(async () => await CheckAndSendNotification(recordId, cameraDestination, cameraLocation));
        }

        public void SpamNotification(Guid recordId, int alarmLevel)
        {
            Task.Run(async () => await SpamNotificationAndCheckFinish(recordId, alarmLevel));
        }

        private async Task EndVotingPhase(Guid recordId)
        {
            DateTime startTime = DateTime.Now;
            bool check = true;
            while (check && (DateTime.UtcNow - startTime).TotalMinutes <= 30)
            {
                Console.WriteLine("========End Voting Phase=====");
                await Task.Delay(5000);
                if (await _memorycachedservice.CheckEnoughVoting(recordId) >= 5)
                {
                    check = false;

                }

                if ((DateTime.Now - startTime).TotalMinutes >= 2)
                {
                    check = false;
                }
            }

            if (!check)
            {
                await _apiCall.AutoCompleteVoting(recordId);
            }

        }
        protected async Task CheckAndSendNotification(Guid recordID, string CameraDestination, string LocationName)
        {
            int countAlarmTime = 0;
            bool check = true;
            var startTime = DateTime.UtcNow;

            while (check && (DateTime.UtcNow - startTime).TotalMinutes <= 30)
            {
                try
                {
                    if (!await _memorycachedservice.CheckRecordKeyIsVote(recordID))
                    {
                        // Perform actions if the record key is not voted
                        NotficationDetailResponse data = await NotificationHandler.Get(11);

                    /*    await CloudMessagingHandlers.CloudMessaging(
                            HandleTextUtil.HandleTitle(data.Title, CameraDestination),
                            HandleTextUtil.HandleContext(data.Context, LocationName, CameraDestination));*/

                        await _memorycachedservice.IncreaseQuantity(recordID, CacheType.FireNotify);

                        Console.WriteLine(countAlarmTime);
                        Console.WriteLine("========Alarm Phase (Phase 1)=====");
                    }
                    else
                    {
                        await Task.Delay(5000); // Delay for 5 seconds before checking again
                        Console.WriteLine("End Task");
                        // If record key is voted, set check to false and exit the loop
                        check = false;
                        //  await _memorycachedservice.SettingCount(recordID, CacheType.FireNotify, countAlarmTime);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    Console.WriteLine($"Error in phase 1: {ex.Message}");
                }

             
            }
            await Task.Delay(5000); // Delay for 5 seconds before checking again
            Console.WriteLine("End Task");

        }

        protected async Task CheckAndAutoAction(Guid recordId)
        {
            var startTime = DateTime.UtcNow;
            bool check = true;
            while (check && (DateTime.UtcNow - startTime).TotalMinutes <= 30)
            {
                try
                {
                    if (!await _memorycachedservice.CheckIsAction(recordId))
                    {
                        //! Send notification to remind manager have some action
                        await CloudMessagingHandlers.CloudMessaging();
                        await _memorycachedservice.IncreaseQuantity(recordId, CacheType.VotingValue);
                        Console.WriteLine("======Action Phase (Phase 2)=======");
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
            }
        }


        protected async Task SpamNotificationAndCheckFinish(Guid recordId, int alarmLevel)
        {
            /*
             Variable have been create before 
            can add fast like the so create the function to quantity++;
             */
            var startTime = DateTime.UtcNow;
            int timeAwait = AwaitTime(alarmLevel);
            bool check = true;
            // NotficationDetailResponse data = await NotificationHandler.Get(alarmLevel);

            while (check && (DateTime.UtcNow - startTime).TotalMinutes <= 30)
            {
                try
                {
                    if (!await _memorycachedservice.CheckIsFinish(recordId))
                    {


                        NotficationDetailResponse data = await NotificationHandler.Get(alarmLevel);
                        Console.WriteLine(data);
                        await CloudMessagingHandlers.CloudMessaging(data.Title, data.Context);
                        await _memorycachedservice.IncreaseQuantity(recordId, TransferCacheType(alarmLevel));
                        Console.WriteLine("=====Action  Phase 3===========");
                    }
                    else
                    {
                        check = false;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: {ex.Message}");
                }
                // await Task.Delay(timeAwait);
                await Task.Delay(5000);
            }
        }
        private static int AwaitTime(int alarmLevel) => alarmLevel switch
        {
            1 => 30000,
            2 => 25000,
            3 => 20000,
            4 => 15000,
            5 => 10000,
        };

        private static CacheType TransferCacheType(int alarmLevel) => alarmLevel switch
        {
            1 => CacheType.AlarmLevel1,
            2 => CacheType.AlarmLevel2,
            3 => CacheType.AlarmLevel3,
            4 => CacheType.AlarmLevel4,
            5 => CacheType.AlarmLevel5,
        };
    }
}
