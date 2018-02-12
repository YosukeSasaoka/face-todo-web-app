using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
 
namespace registerPerson
{
    [JsonObject]
    public class PersonId
    {
        [JsonProperty("personId")]
        public string Id { get; set; }
    }
}
