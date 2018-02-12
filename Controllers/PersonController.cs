using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using registerPerson.Models;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;

using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;

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
                SubscriptionKey = "write here subscription key",
                GroupName = "group1",
            };

            var task = Task.Run(() =>
            {
                var personIdJson = faceApi.createPerson(person.Name).Result;
                PersonId personId = JsonConvert.DeserializeObject<PersonId>(personIdJson);
                return faceApi.addPersonFace(personId.Id, person.FaceImgUrl);
            });
            Console.WriteLine(task.Result);
            return task.Result;
        }
    }
}
