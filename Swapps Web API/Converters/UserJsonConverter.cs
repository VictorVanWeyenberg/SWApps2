using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swapps_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Converters
{
    public class UserJsonConverter : JsonConverter<User>
    {
        public override User ReadJson(JsonReader reader, Type objectType, User existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            List<Establishment> subscriptions = new List<Establishment>();
            if (jObject.Value<JObject>("Subscriptions") != null)
            {
                foreach (JObject jsonEst in jObject.Value<JObject[]>("Subscriptions"))
                {

                    Establishment est = JsonConvert.DeserializeObject<Establishment>(jsonEst.ToString());
                    subscriptions.Add(est);
                }
            }
            var jsonString = jObject.ToString();
            int id = jObject.Value<int>("ID");
            string firstName = jObject.Value<string>("FirstName");
            string lastName = jObject.Value<string>("LastName");
            string email = jObject.Value<string>("Email");
            return new User
            {
                AbstractUser = new AbstractUser
                {
                    ID = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                },
                Subscriptions = subscriptions
            };
        }

        public override void WriteJson(JsonWriter writer, User value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
