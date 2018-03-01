using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using faceTodoApplication.Models;

namespace faceTodoApplication.Models
{
    public class AddPerson
    {
        public string addPerson(string subKey, string gName, string name, string imgUrl)
        {

            var faceApi = new FaceApi()
            {
                SubscriptionKey = subKey,
                GroupName = gName,
            };

            var personIdJson = faceApi.createPerson(name).Result;
            PersonId personId = JsonConvert.DeserializeObject<PersonId>(personIdJson);
            var adsImage = faceApi.addPersonFace(personId.Id, imgUrl);
            return personId.Id;

        }
    }

}
