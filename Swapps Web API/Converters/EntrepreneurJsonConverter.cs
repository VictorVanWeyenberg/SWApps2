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
    public class EntrepreneurJsonConverter : JsonConverter<Entrepreneur>
    {
        public override Entrepreneur ReadJson(JsonReader reader, Type objectType, Entrepreneur existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            Establishment est = null;
            if (jObject.Value<JObject>("Establishment") != null)
            {
                est = JsonConvert.DeserializeObject<Establishment>(jObject.Value<JObject>("Establishment").ToString());
            }
            var jsonString = jObject.ToString();
            int id = jObject.Value<JObject>("User").Value<int>("ID");
            string firstName = jObject.Value<JObject>("User").Value<string>("FirstName");
            string lastName = jObject.Value<JObject>("User").Value<string>("LastName");
            string email = jObject.Value<JObject>("User").Value<string>("Email");
            return new Entrepreneur
            {
                User = new AbstractUser
                {
                    ID = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                },
                Establishment = est
            };
        }

        public override void WriteJson(JsonWriter writer, Entrepreneur value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
