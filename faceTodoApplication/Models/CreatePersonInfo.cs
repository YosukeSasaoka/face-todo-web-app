using System;
using Newtonsoft.Json;
namespace faceTodoApplication.Models
{
    public class CreatePersonInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
