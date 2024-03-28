using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler
{
 
    public static class CloudMessagingHandlers
    {

        public  static async Task CloudMessaging(string? titleInput = "Fire Alarm Detect",string? bodyInput = "Finding Alarm in Location B",string fcm_token = "dqLIh9cjR0K8qf7ZY-CJan:APA91bFR5f-OI5f1S3e00YqUrAOx_LV2vdvmPXq9VHWY0zQFFVK55JwjNaVlkDlsZd8jwrO_SmVH1tvYv8zqIEfAC-LIlQNIxq4q1v8H884hMY7nfX2WJuBd1XFrfpG96iBswLSd7E7p")
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAUS72wWs:APA91bFaHPMWQR3SzxCTpEpY7cPUq2NfmYWFKGYRwABpXXu8NGLrL6TJW4rsT1b9K3RKGZ7XoRGOIIXIcek254ZHHhsVQX2OSgT4hxWSh4Sm4g1-XZ_UGiL5tEe98knp_7OKzkQwwxzb"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "348680274283"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = fcm_token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = bodyInput,
                    title = titleInput,
                    badge = 1
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }

            };
            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
        //Todo still doing
        public static async Task PushNotification(string? titleInput, string? bodyInput, string fcm_token)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAUS72wWs:APA91bFaHPMWQR3SzxCTpEpY7cPUq2NfmYWFKGYRwABpXXu8NGLrL6TJW4rsT1b9K3RKGZ7XoRGOIIXIcek254ZHHhsVQX2OSgT4hxWSh4Sm4g1-XZ_UGiL5tEe98knp_7OKzkQwwxzb"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "348680274283"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = fcm_token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = bodyInput,
                    title = titleInput,
                    badge = 1
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }

            };
            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
    }
}
