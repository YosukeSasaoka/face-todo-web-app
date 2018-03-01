using System;
using Newtonsoft.Json;

namespace faceTodoApplication.Models
{
    [JsonObject]
    public class TodoDetail
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("date")]
        public DateTime DoDate { get; set; }
    }
}