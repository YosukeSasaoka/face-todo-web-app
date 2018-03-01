using Newtonsoft.Json;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class IdentifyResponse
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }
        [JsonProperty("candidates")]
        public Candidates CandidatesInfo { get; set;}
    }
}
