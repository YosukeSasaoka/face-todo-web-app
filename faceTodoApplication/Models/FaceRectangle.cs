using Newtonsoft.Json;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class FaceRectangle
    {
        [JsonProperty("width")]
        public string width { get; set; }
        [JsonProperty("height")]
        public string height { get; set; }
        [JsonProperty("left")]
        public string left { get; set; }
        [JsonProperty("top")]
        public string top { get; set; }
    }
}
