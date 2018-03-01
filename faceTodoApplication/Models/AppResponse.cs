using Newtonsoft.Json;


namespace registerPerson
{
    [JsonObject]
    public class AppResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
        [JsonProperty("todo")]
        public TodoDetail TodoList { get; set; }
    }
}
