using System;
using Newtonsoft.Json;

namespace registerPerson
{
    [JsonObject]
    public class Todo
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("date")]
        public DateTime DoDate { get; set; }
    }
}
