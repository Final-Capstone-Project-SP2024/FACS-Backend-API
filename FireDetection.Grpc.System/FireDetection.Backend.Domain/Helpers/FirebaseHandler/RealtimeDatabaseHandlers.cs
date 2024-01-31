using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            FirebaseResponse response =  await _client.GetAsync($@"Users/{userId}");
            return response.Body.ToString();
        }
    }
}
