using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using faceTodoApplication.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace faceTodoApplication.Controllers
{
    [Route("api/[controller]")]
    public class FaceController : Controller
    {
        private readonly TodoesContext _context;
        public FaceController(TodoesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Task<string> Post([FromBody]Detect detect)
        {
            string appKey = ConfigurationManager.AppSettings["appKey"];

            var faceApi = new FaceApi()
            {
                SubscriptionKey = "",
                GroupName = ""
            };



            var task = Task.Run(() =>
            {
                AppResponse todoList;
                try{
                    
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

                    TodoDetail todo = new TodoDetail()
                    {
                        Title = getTodoTitle(personId).Result,
                    };

                    if (TodoDbExistPersonId(personId))
                    {
                        todoList = new AppResponse()
                        {
                            PersonId = personId,
                            FaceRectangleInfo = faceRectangle,
                            TodoList = todo,
                            Show = true,
                        };
                    }
                    else
                    {
                        todoList = new AppResponse()
                        {
                            PersonId = null,
                            FaceRectangleInfo = null,
                            TodoList = null,
                            Show = false,
                        };
                    }

                    string json = JsonConvert.SerializeObject(todoList);
                    return json;
                } catch(Exception e) {
                    Console.WriteLine(e);
                    todoList = new AppResponse()
                    {
                        PersonId = null,
                        FaceRectangleInfo = null,
                        TodoList = null,
                        Show = false,
                    };
                    string json = JsonConvert.SerializeObject(todoList);
                    return json;
                }
            });
            return task;
        }

        public bool TodoDbExistPersonId(string personId)
        {
            return _context.Todo.Any(e => e.PersonId.Contains(personId));
        }

        public async Task<string> getTodoTitle(string personId) {
            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.PersonId.Contains(personId));
            return todo.TodoTitle;
        }
    }
}
