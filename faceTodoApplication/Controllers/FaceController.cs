using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using faceTodoApplication.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace faceTodoApplication.Controllers
{
    [Route("api/[controller]")]
    public class FaceController : Controller
    {
        [HttpPost]
        public Task<string> Post([FromBody]Detect detect)
        {
            string appKey = ConfigurationManager.AppSettings["appKey"];

            var faceApi = new FaceApi()
            {
                SubscriptionKey = "",
                GroupName = ""
            };

            var todo1 = new TodoDetail()
            {
                Title = "hoge",
                DoDate = DateTime.Now
            };

            var todo2 = new TodoDetail()
            {
                Title = "fuga",
                DoDate = DateTime.Today
            };

            var task = Task.Run(() =>
            {
                // 顔検出
                var detectResponseJson = faceApi.detectPersonFace(detect.FaceUrl).Result;
                DetectResponse detectResponse = JsonConvert.DeserializeObject<DetectResponse>(detectResponseJson);

                // 顔識別
                var identifyResponseJson = faceApi.identifyPersonFace(detectResponse.FaceId).Result;
                IdentifyResponse identifyResponse = JsonConvert.DeserializeObject<IdentifyResponse>(identifyResponseJson);

                var todoList = new AppResponse()
                {
                    DetectResponseInfo = detectResponse,
                    IdentifyResponseInfo = identifyResponse,
                    TodoList = todo1
                };

                string json = JsonConvert.SerializeObject(todoList);
                return json;
            });
            return task;
        }
    }
}
