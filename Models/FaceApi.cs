using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;
using System.Text;
using System.Net.Http;
using System.Collections;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http.Headers;

namespace registerPerson.Models
{
    public class FaceApi
    {
        public string SubscriptionKey { get; set; }
        public string GroupName { get; set; }

        async public Task<string> createPerson(String name)
        {
            var personInfo = new CreatePersonInfo()
            {
                Name = name,
            };

            var json = JsonConvert.SerializeObject(personInfo);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ($"https://australiaeast.api.cognitive.microsoft.com/face/v1.0/persongroups/{GroupName}/persons"));
                request.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var res = await client.SendAsync(request);
                return res.Content.ReadAsStringAsync().Result;
            }
        }

        async public Task<string> addPersonFace(String personId, String faceImgUrl)
        {
            var personInfo = new AddPersonFaceInfo()
            {
                Url = faceImgUrl,
            };

            var json = JsonConvert.SerializeObject(personInfo);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ($"https://australiaeast.api.cognitive.microsoft.com/face/v1.0/persongroups/{GroupName}/persons/{personId}/persistedFaces"));
                request.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var res = await client.SendAsync(request);
                return res.Content.ReadAsStringAsync().Result;
            }
        }

        async public Task<string> detectPersonFace(String imgUrl)
        {
            var personInfo = new AddPersonFaceInfo()
            {
                Url = imgUrl,
            };

            var json = JsonConvert.SerializeObject(personInfo);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ($"https://australiaeast.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true"));
                request.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var res = await client.SendAsync(request);
                return res.Content.ReadAsStringAsync().Result;
            }
        }

    }
}
