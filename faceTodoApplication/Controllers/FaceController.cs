using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using faceTodoApplication.Models;
using System.Collections.Generic;
using System.Linq;


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
                var detectResponse = JsonConvert.DeserializeObject<List<DetectResponse>>(detectResponseJson);

                // 顔識別
                var identifyResponseJson = faceApi.identifyPersonFace(detectResponse[0].FaceId).Result;
                var identifyResponse = JsonConvert.DeserializeObject<List<IdentifyResponse>>(identifyResponseJson);

                //faceRectangleのみ抽出
                FaceRectangle faceRectangle = detectResponse[0].FaceRectangle;

                // PersonIdのみ抽出
                var candidates = identifyResponse[0].CandidatesInfo;
                string personId = candidates[0].PersonId;

                var todoList = new AppResponse()
                {
                    PersonId = personId,
                    FaceRectangleInfo =faceRectangle,
                    TodoList = todo1
                };

                string json = JsonConvert.SerializeObject(todoList);
                return json;
            });
            return task;
        }
    }
}
