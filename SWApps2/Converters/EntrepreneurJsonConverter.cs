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
                est = JsonConvert.DeserializeObject<Establishment>(jObject.Value<JObject>("Establishment").ToString(), new EstablishmentJsonConverter());
            }
            var jsonString = jObject.ToString();
            string firstName = jObject.Value<JObject>("User").Value<string>("FirstName");
            string lastName = jObject.Value<JObject>("User").Value<string>("LastName");
            string email = jObject.Value<JObject>("User").Value<string>("Email");
            return new Entrepreneur(firstName, lastName, email, est);
        }

        public override void WriteJson(JsonWriter writer, Entrepreneur value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
