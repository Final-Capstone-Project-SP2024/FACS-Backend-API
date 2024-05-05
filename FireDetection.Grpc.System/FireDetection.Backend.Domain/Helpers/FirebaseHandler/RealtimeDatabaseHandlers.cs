using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Object = System.Object;

namespace FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler
{
    public static class RealtimeDatabaseHandlers
    {
        private readonly static string authSecret = "49tixdsWTpWSVXWdgjfrJPRnvwSKIqUvzC8D1Z2v";
        private readonly static string basePath = "https://final-capstone-project-f8bdd-default-rtdb.asia-southeast1.firebasedatabase.app/";

        static IFirebaseClient _client;
        static IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = authSecret,
            BasePath = basePath

        };
        public static async Task<string> GetFCMTokenByUserID(Guid userId = new Guid())
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.GetAsync($@"FCMToken/{userId}");
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "NotUserloginInthis Account";
            }
            return response.Body.ToString();
        }

        public static async Task AddNew(Object newObject, string nameObject)
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.SetAsync($@"Notifications/{nameObject}", newObject);
            response.Body.ToString();
        }

        public static async Task Update(Object newObject,string nameObject)
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.UpdateAsync($@"Notifications/{nameObject}", newObject);
            response.Body.ToString();
        }


        public static async Task AddFCMToken(Guid userId,string token)
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.SetAsync($@"FCMToken/{userId}", token);
            
            response.Body.ToString();
        }

        public static async Task DeleteFCMToken(Guid userId)
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.DeleteAsync($@"FCMToken/{userId}");

            response.Body.ToString();
        }

        public static async Task<string> GetFCMToken(Guid? userId = default )
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.GetAsync($@"FCMToken/{userId}");
             return response.Body.ToString();

        }
        public static async Task<NotificationListResponse> GetNotifications()
        {
            List<string> notificationListResponse = new();
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.GetAsync("Notifications");

            response.Body.ToArray();
            JObject jsonObject = JObject.Parse(response.Body.ToString());


            foreach (var item in jsonObject)
            {
                string key = item.Key;
                notificationListResponse.Add(key);
            }
            return new NotificationListResponse
            {
                Notifications = notificationListResponse
            };
        }


        public static async Task<NotficationDetailResponse> GetDetail(string header)
        {
            _client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await _client.GetAsync($@"Notifications/{header}");

            JObject jsonObject = JObject.Parse(response.Body.ToString());
            string context = (string)jsonObject["context"];
            string title = (string)jsonObject["title"];
            return new NotficationDetailResponse
            {
                Context = context,
                Name = header,
                Title = title
            };
        }
    }


    public static class NotificationHandler
    {
        public static async Task AddNewNotification(string Title, string Context, string NotificationName)
        {
            var newNotification = new
            {
                title = Title,
                context = Context
            };

            await RealtimeDatabaseHandlers.AddNew(newNotification, NotificationName);
        }

        public static async Task UpdateNotification(int id,string Title , string Context)
        {
            string numberString = null;
            switch (id)
            {
                case 1:
                    numberString = AlarmType.Level1;
                    break;
                case 2:
                    numberString = AlarmType.Level2;
                    break;
                case 3:
                    numberString = AlarmType.Level3;
                    break;
                case 4:
                    numberString = AlarmType.Level4;
                    break;
                case 5:
                    numberString = AlarmType.Level5;
                    break;
            }

            var updateNotification = new
            {
                title = Title,
                context = Context
            };
            await RealtimeDatabaseHandlers.Update(updateNotification, numberString);
        }

        public static async Task<NotificationListResponse> GetAll()
        {

            return await RealtimeDatabaseHandlers.GetNotifications();
        }


        public static async Task<NotficationDetailResponse> Get(int id)
        {
            switch (id)
            {
                case 1: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Level1);
                case 2: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Level2);
                case 3: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Level3);
                case 4: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Level4);
                case 5: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Level5);
                case 7: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.DisconnectCamera);
                case 8: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.FakeAlarm);
                case 9: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Voting);
                case 10: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.Remind);
                case 11: return await RealtimeDatabaseHandlers.GetDetail(AlarmType.FireDetectNotify);
                default:
                    break;
            }

            return await RealtimeDatabaseHandlers.GetDetail("Alarm level 1");

        }
    }
}
