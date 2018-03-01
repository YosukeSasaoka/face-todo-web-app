using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace faceTodoApplication.Models
{
    public class IdentifyPersonFace
    {
        [JsonProperty("faceIds")]
        public List<String> FaceIds { get; set; }
        [JsonProperty("personGroupId")]
        public string PersonGroupId { get; set; }
        [JsonProperty("maxNumOfCandidatesReturned")]
        public int MaxNumOfCandidatesReturned { get; set; }
    }
}
