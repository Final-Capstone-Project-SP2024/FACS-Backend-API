using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class APICallService : IAPICallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly static string httpLink = "https://localhost:4222";

        public APICallService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AutoCallAction(Guid recordId, int actionId)
        {
            using (HttpClient client = new HttpClient())
            {
                string Link = httpLink + $"/Record/{recordId}/action";
                string jsonContent = $@"{{
                    ""actionId"": {actionId},
                    ""userID"": ""3a232872-67d1-43ae-9629-fb65bdc663cd""
                }}";
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Link, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }

        public async Task AutoCompleteVoting(Guid recordId)
        {
            using (HttpClient client = new HttpClient())
            {
                string Link = httpLink + $"/Record/{recordId}/endvote";
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Link,content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }
    }
}
