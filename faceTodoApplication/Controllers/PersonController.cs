using System;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using faceTodoApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace faceTodoApplication.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        [HttpPost]
        public string Post([FromBody]Person person)
        {
            string appKey = ConfigurationManager.AppSettings["appKey"];

            var faceApi = new FaceApi()
            {
                SubscriptionKey = "",
                GroupName = "",
            };

            var task = Task.Run(() =>
            {
                var personIdJson = faceApi.createPerson(person.Name).Result;
                PersonId personId = JsonConvert.DeserializeObject<PersonId>(personIdJson);
                var res = faceApi.addPersonFace(personId.Id, person.FaceImgUrl);
                faceApi.trainPersonFace();
                return res;
            });

            return task.Result;
        }
    }
}
