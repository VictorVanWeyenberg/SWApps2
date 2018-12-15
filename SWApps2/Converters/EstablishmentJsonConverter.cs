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
    public class EstablishmentJsonConverter : JsonConverter<Establishment>
    {

        public override Establishment ReadJson(JsonReader reader, Type objectType, Establishment existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            Establishment est = null;
            try
            {
                string name = jObject.Value<string>("Name");
                List<string> tags = jObject.Value<JArray>("Tags").ToList().Select<JToken, string>(t => (t as dynamic)?.Value).ToList();
                Address address = jObject.Value<JToken>("Address").ToObject<Address>();

                ServiceHours jsonServiceHours = JsonConvert.DeserializeObject<ServiceHours>(jObject.ToString(), new ServiceHoursJsonConverter());

                List<EstablishmentEvent> events = new List<EstablishmentEvent>();
                List<Promotion> promotions = new List<Promotion>();

                foreach (JObject jsonEvent in jObject.Value<JArray>("Events").ToObject<List<JObject>>()) events.Add(JsonConvert.DeserializeObject<EstablishmentEvent>(jsonEvent.ToString(), new EstablishmentEventJsonConverter()));
                foreach (JObject jsonPromotion in jObject.Value<JArray>("Promotions").ToObject<List<JObject>>()) promotions.Add(JsonConvert.DeserializeObject<Promotion>(jsonPromotion.ToString(), new PromotionJsonConverter()));

                EstablishmentType type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), jObject.Value<string>("Type"));
                est = new Establishment(name, address, jsonServiceHours, type, tags, promotions, events);

                foreach (EstablishmentEvent eventje in events) eventje.Establishment = est;
                foreach (Promotion promotion in promotions) promotion.Establishment = est;
            } catch (NullReferenceException)
            {

            } catch (ArgumentNullException)
            {

            }

            return est;
        }

        public override void WriteJson(JsonWriter writer, Establishment value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
