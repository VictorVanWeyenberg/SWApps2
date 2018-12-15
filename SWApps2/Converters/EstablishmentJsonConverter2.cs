using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Converters
{
    public class EstablishmentJsonConverter2 : JsonConverter<Establishment>
    {
        public override Establishment ReadJson(JsonReader reader, Type objectType, Establishment existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            return MapJsonToEstablishment(jObject);
        }

        private Establishment MapJsonToEstablishment(JObject content)
        {
            int ID = content.Value<int>("EstablishmentID");
            string name = content.Value<string>("Name");
            EstablishmentType type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), content.Value<string>("Type"));
            Address address = new Address(content.Value<string>("AddressStreet"), content.Value<int>("AddressNumber"));
            IList<string> tags = new List<string>();
            foreach (JObject obj in content.Value<JArray>("Tags"))
            {
                tags.Add(obj.Value<string>("Value"));
            }
            IList<Promotion> promotions = new List<Promotion>();
            foreach (JObject obj in content.Value<JArray>("Promotions"))
            {
                promotions.Add((Promotion)MapJObjectToEvent(obj, nameof(Promotion)));
            }
            IList<EstablishmentEvent> events = new List<EstablishmentEvent>();
            foreach (JObject obj in content.Value<JArray>("Events"))
            {
                events.Add((EstablishmentEvent)MapJObjectToEvent(obj, nameof(EstablishmentEvent)));
            }
            ServiceHours hours = ParseServiceHoursFromJson(content.Value<JArray>("ServiceHours"));
            //Create establishment, add it to promos and events, add promos and events to establishment, return it
            Establishment establishment = new Establishment(name, address, hours, type, null);
            foreach (Promotion promo in promotions)
            {
                promo.Establishment = establishment;
            }
            foreach (EstablishmentEvent evt in events)
            {
                evt.Establishment = establishment;
            }
            establishment.Promotions = (List<Promotion>)promotions;
            establishment.EstablishmentEvents = (List<EstablishmentEvent>)events;
            establishment.ID = ID;
            return establishment;
        }

        private ServiceHours ParseServiceHoursFromJson(JArray input)
        {
            TimeInterval[] intervals = new TimeInterval[7];
            foreach (JObject obj in input)
            {
                int index = obj.Value<int>("DayOfWeek");
                if (!obj.Value<bool>("IsClosed"))//if not closed parse interval
                {
                    int sH = obj.Value<int>("StartHour");
                    int sM = obj.Value<int>("StartMinute");
                    int eH = obj.Value<int>("EndHour");
                    int eM = obj.Value<int>("EndMinute");
                    intervals[index] = new TimeInterval(new LocalTime(sH, sM), new LocalTime(eH, eM));
                }
            }
            return new ServiceHours(intervals);
        }

        private Event MapJObjectToEvent(JObject input, string type)
        {
            string name = input.Value<string>("Name");
            string description = input.Value<string>("Description");
            DateTime start = DateTime.Parse(input.Value<string>("StartDate"));
            DateTime end = DateTime.Parse(input.Value<string>("EndDate"));
            switch (type)
            {
                case nameof(Promotion): return new Promotion(null, name, description, start, end);
                case nameof(EstablishmentEvent): return new EstablishmentEvent(null, name, description, start, end);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, Establishment value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
