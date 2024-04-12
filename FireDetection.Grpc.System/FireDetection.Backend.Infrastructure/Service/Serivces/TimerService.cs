using Firebase.Auth;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private ILocationScopeService _locationScopeService;
        public TimerService(IMemoryCacheService memoryCacheService, IAPICallService aPICall, ILocationScopeService locationScopeService)
        {
            _memorycachedservice = memoryCacheService;
            _apiCall = aPICall;
            _locationScopeService = locationScopeService;
        }


        public void EndVotePhase(Guid recordId)
        {
            Task.Run(async () => await EndVotingPhase(recordId));
        }
        public void CheckIsAction(Guid recordId, List<Guid> users)
        {
            Task.Run(async () => await CheckAndAutoAction(recordId,users));
        }


        //? using to phase 1: alarm Notify to user in this camera and Manager
        public  void CheckIsVoting(Guid recordId, string cameraDestination, string cameraLocation,List<Guid> users)
        {
            Task.Run(async () => await CheckAndSendNotification(recordId, cameraDestination, cameraLocation,users));
        }

        public void SpamNotification(Guid recordId, int alarmLevel, List<Guid> users)
        {
            Task.Run(async () => await SpamNotificationAndCheckFinish(recordId, alarmLevel,users));
        }

        public void DisconnectionNotification(Guid recordId, string cameraDestination, string cameraLocation)
        {
            Task.Run(async () => await DisconnectedNotificiationAlert(recordId, cameraDestination, cameraLocation));
        }

        protected async Task DisconnectedNotificiationAlert(Guid recordId, string cameraDestination, string cameraLocation)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.UtcNow - start).TotalMinutes <= 1)
            {
                NotficationDetailResponse data = await NotificationHandler.Get(7);
                Dictionary<string, string> tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(await RealtimeDatabaseHandlers.GetFCMToken());
                List<string> values = new List<string>(tokens.Values);

                // Print the values
                foreach (var value in values)
                {

                    Console.WriteLine(data);
                    await CloudMessagingHandlers.CloudMessaging(data.Title, data.Context, value);
                }
               await  Task.Delay(5000);
            
            }
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
        protected async Task CheckAndSendNotification(Guid recordID, string CameraDestination, string LocationName, List<Guid> users)
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

                        //? get all token in real-time database
                        foreach (var item in users)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine(HandleTextUtil.HandleTitle(data.Title, CameraDestination));
                            Console.WriteLine(HandleTextUtil.HandleContext(data.Context, LocationName, CameraDestination));
                            string token = await RealtimeDatabaseHandlers.GetFCMTokenByUserID(item);

                            Console.Write(token);
                            await CloudMessagingHandlers.CloudMessaging(
                                HandleTextUtil.HandleTitle(data.Title, CameraDestination),
                                HandleTextUtil.HandleContext(data.Context, LocationName, CameraDestination),
                               token.Replace("\"", ""));
                        }

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
            Console.WriteLine("+++++++++++++++++++++++++++++++++");

        }

        protected async Task CheckAndAutoAction(Guid recordId,List<Guid> users)
        {
            var startTime = DateTime.UtcNow;
            bool check = true;
            while (check && (DateTime.UtcNow - startTime).TotalMinutes <= 30)
            {
                try
                {
                    if (!await _memorycachedservice.CheckIsAction(recordId))
                    {
                        NotficationDetailResponse data = await NotificationHandler.Get(9);
                        foreach (var user in users)
                        {
                            Console.WriteLine(user);
                            Console.WriteLine(data.Context);
                            Console.WriteLine(data.Title);
                            string token = await RealtimeDatabaseHandlers.GetFCMTokenByUserID(user);
                            await CloudMessagingHandlers.CloudMessaging(data.Title,data.Context,fcm_token: token.Replace("\"", ""));
                            
                        }
                        
                       // await _memorycachedservice.IncreaseQuantity(recordId, CacheType.VotingValue);
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


        protected async Task SpamNotificationAndCheckFinish(Guid recordId, int alarmLevel, List<Guid> users)
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
                        foreach (var item in users)
                        {
                            NotficationDetailResponse data = await NotificationHandler.Get(alarmLevel);
                            Console.WriteLine(item);
                            Console.WriteLine(data.Context);
                            Console.WriteLine(data.Title);
                            string token = await RealtimeDatabaseHandlers.GetFCMTokenByUserID(item);
                            await CloudMessagingHandlers.CloudMessaging(data.Title, data.Context, token.Replace("\"", ""));
                        }

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
