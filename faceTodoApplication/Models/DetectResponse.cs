using Newtonsoft.Json;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class DetectResponse
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }
        [JsonProperty("faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
    }
}
