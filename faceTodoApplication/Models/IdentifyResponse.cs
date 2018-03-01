using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class IdentifyResponse
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }
        [JsonProperty("candidates")]
        public List<Candidates> CandidatesInfo { get; set;}
    }
}
