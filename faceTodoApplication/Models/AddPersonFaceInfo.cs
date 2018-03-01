using System;
using Newtonsoft.Json;

namespace faceTodoApplication.Models
{
    public class AddPersonFaceInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
