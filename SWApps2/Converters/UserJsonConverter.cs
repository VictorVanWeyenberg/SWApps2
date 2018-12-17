using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWApps2.Model;
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
            if (jObject.Value<JArray>("Subscriptions") != null)
            {
                foreach (JObject jsonEst in jObject.Value<JArray>("Subscriptions"))
                {

                    Establishment est = JsonConvert.DeserializeObject<Establishment>(jsonEst.ToString(), new EstablishmentJsonConverter());
                    subscriptions.Add(est);
                }
            }
            var jsonString = jObject.ToString();
            int id = jObject.Value<JObject>("AbstractUser").Value<int>("ID");
            string firstName = jObject.Value<JObject>("AbstractUser").Value<string>("FirstName");
            string lastName = jObject.Value<JObject>("AbstractUser").Value<string>("LastName");
            string email = jObject.Value<JObject>("AbstractUser").Value<string>("Email");
            return new User(id, firstName, lastName, email, subscriptions);
        }

        public override void WriteJson(JsonWriter writer, User user, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(user, new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            if (t.Type != JTokenType.Object) {
                t.WriteTo(writer);
            } else {
                JObject jObject = (JObject)t;
                //jObject.Add(new JProperty("ID", user.ID));
                //jObject.Add(new JProperty("FirstName", user.FirstName));
                //jObject.Add(new JProperty("LastName", user.LastName));
                //jObject.Add(new JProperty("Email", user.Email));
                //jObject.Add(new JProperty("Subscriptions", user.SubsribedEstablishments));
                jObject.WriteTo(writer);
            }
        }
    }
}
