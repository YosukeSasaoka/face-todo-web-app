using System;
using Newtonsoft.Json;
namespace registerPerson
{
    public class CreatePersonInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
