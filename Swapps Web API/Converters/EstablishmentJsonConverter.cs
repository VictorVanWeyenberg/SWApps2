using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swapps_Web_API.Models;
using SWApps2.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Converters
{
    public class EstablishmentJsonConverter : JsonConverter<Establishment>
    {
        public override Establishment ReadJson(JsonReader reader, Type objectType, Establishment existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            int id = jObject.Value<int>("ID");
            string name = jObject.Value<string>("Name");
            List<Tag> tags = jObject.Value<JArray>("Tags").ToList().Select<JToken, Tag>(t => new Tag { Value = (t as dynamic)?.Value }).ToList();
            Address address = jObject.Value<JToken>("Address").ToObject<Address>();

            List<TimeInterval> serviceHours = new List<TimeInterval>();
            List<EstablishmentEvent> events = new List<EstablishmentEvent>();
            List<Promotion> promotions = new List<Promotion>();

            foreach (JObject jsonTimeInterval in jObject.Value<JObject>("ServiceHours").Value<JArray>("Hours")) serviceHours.Add(JsonConvert.DeserializeObject<TimeInterval>(jsonTimeInterval.ToString(), new TimeIntervalJsonConverter()));
            foreach (JObject jsonEvent in jObject.Value<JArray>("EstablishmentEvents").ToObject<List<JObject>>()) events.Add(JsonConvert.DeserializeObject<EstablishmentEvent>(jsonEvent.ToString(), new EstablishmentEventJsonConverter()));
            foreach (JObject jsonPromotion in jObject.Value<JArray>("Promotions").ToObject<List<JObject>>()) promotions.Add(JsonConvert.DeserializeObject<Promotion>(jsonPromotion.ToString(), new PromotionJsonConverter()));

            EstablishmentType type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), jObject.Value<string>("Type"));
            Establishment est = new Establishment
            {
                ID = id,
                Name = name,
                Address = address,
                ServiceHours = serviceHours,
                Type = type,
                Tags = tags,
                Promotions = promotions,
                Events = events
            };
                // (id, name, address, serviceHours, type, tags, promotions, events);

            foreach (EstablishmentEvent eventje in events) eventje.Establishment = est;
            foreach (Promotion promotion in promotions) promotion.Establishment = est;

            return est;
        }

        public override void WriteJson(JsonWriter writer, Establishment value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}