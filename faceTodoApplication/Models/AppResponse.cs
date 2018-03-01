
using faceTodoApplication.Models;
using Newtonsoft.Json;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class AppResponse
    {
        [JsonProperty("detectResponse")]
        public DetectResponse DetectResponseInfo { get; set; }
        [JsonProperty("IdentifyResponse")]
        public IdentifyResponse IdentifyResponseInfo { get; set; }
        [JsonProperty("todo")]
        public TodoDetail TodoList { get; set; }
    }
}
