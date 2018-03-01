using Newtonsoft.Json;


namespace faceTodoApplication.Models
{
    [JsonObject]
    public class Candidates
    {
        [JsonProperty("personId")]
        public string PersonId { get; set; }
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }
}
