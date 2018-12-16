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
    public class PromotionJsonConverter : JsonConverter<Promotion>
    {
        public override Promotion ReadJson(JsonReader reader, Type objectType, Promotion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            string name = jObject.Value<string>("Name");
            string description = jObject.Value<string>("Description");
            DateTime startDate = jObject.Value<DateTime>("StartDate");
            DateTime endDate = jObject.Value<DateTime>("EndDate");
            Establishment est = null;
            try
            {
                est = JsonConvert.DeserializeObject<Establishment>(
                    jObject.Value<JObject>("Establishment").ToString(),
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Converters = new List<JsonConverter>()
                        {
                        new EstablishmentJsonConverter()
                        }
                    });
            }
            catch (ArgumentException) { }
            catch (NullReferenceException) { }
            return new Promotion(est, name, description, startDate, endDate);
        }

        public override void WriteJson(JsonWriter writer, Promotion value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
