﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using registerPerson.Models;
using System.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace registerPerson.Controllers
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
                SubscriptionKey = "write key",
                GroupName = "write froup name",
            };

            var todo1 = new Todo()
            {
                Title = "hoge",
                DoDate = DateTime.Now
            };

            var todo2 = new Todo()
            {
                Title = "fuga",
                DoDate = DateTime.Today
            };

            var task = Task.Run(() =>
            {
                var response = faceApi.detectPersonFace(detect.FaceUrl).Result;
                var todoList = new AppResponse()
                {
                    Response = response,
                    TodoList = todo1
                };

                string json = JsonConvert.SerializeObject(todoList);
                return json;
            });
            return task;
        }
    }
}