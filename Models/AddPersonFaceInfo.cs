using System;
using Newtonsoft.Json;

namespace registerPerson
{
    public class AddPersonFaceInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
