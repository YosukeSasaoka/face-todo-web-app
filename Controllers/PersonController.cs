using System;
using Microsoft.AspNetCore.Mvc;
using registerPerson.Models;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace registerPerson.Controllers
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
                SubscriptionKey = "write key",
                GroupName = "write froup name",
            };

            var task = Task.Run(() =>
            {
                var personIdJson = faceApi.createPerson(person.Name).Result;
                PersonId personId = JsonConvert.DeserializeObject<PersonId>(personIdJson);
                return faceApi.addPersonFace(personId.Id, person.FaceImgUrl);
            });

            return task.Result;
        }
    }
}
