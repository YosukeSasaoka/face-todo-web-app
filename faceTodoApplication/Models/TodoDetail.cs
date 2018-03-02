using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace faceTodoApplication.Models
{
    [JsonObject]
    public class TodoDetail
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}