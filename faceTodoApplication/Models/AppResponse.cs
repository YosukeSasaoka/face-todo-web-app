
using faceTodoApplication.Models;
using Newtonsoft.Json;

namespace faceTodoApplication.Models
{
    [JsonObject]
    public class AppResponse
    {
        [JsonProperty("personId")]
        public string PersonId { get; set; }
        [JsonProperty("faceRectangle")]
        public FaceRectangle FaceRectangleInfo { get; set; }
        [JsonProperty("todo")]
        public TodoDetail TodoList { get; set; }
        [JsonProperty("show")]
        public bool Show { get; set; }
    }
}
