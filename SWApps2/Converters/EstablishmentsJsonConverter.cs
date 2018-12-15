using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Converters
{
    public class EstablishmentsJsonConverter : JsonConverter<Establishment[]>
    {
        public override Establishment[] ReadJson(JsonReader reader, Type objectType, Establishment[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            IList<Establishment> establishments = new List<Establishment>();
            foreach (JObject content in jObject.Value<JArray>("Establishments"))
            {
                
                establishments.Add(MapJsonToEstablishment(content));
            }
            return establishments.ToArray();
        }

        private Establishment MapJsonToEstablishment(JObject content)
        {
            int ID = content.Value<int>("EstablishmentID");
            string name = content.Value<string>("Name");
            EstablishmentType type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), content.Value<string>("Type"));
            Address address = new Address(content.Value<string>("AddressStreet"), content.Value<int>("AddressNumber"));
            List<string> tags = (List<string>)content.Value<JArray>("Tags").ToList().Select(t => t.Value<string>());
            //promotions -> add the establishment to the promotion too!
            List<Promotion> promotions = (List<Promotion>)content.Value<JArray>("Promotions").ToList().Select(obj => (Promotion)MapJObjectToEvent(obj as JObject,nameof(Promotion)));
            //events -> add the establishment to event too
            List<EstablishmentEvent> events = (List<EstablishmentEvent>)content.Value<JArray>("Events").ToList().Select(obj => (EstablishmentEvent)MapJObjectToEvent(obj as JObject, nameof(EstablishmentEvent)));
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
            establishment.Promotions = promotions;
            establishment.EstablishmentEvents = events;
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

        public override void WriteJson(JsonWriter writer, Establishment[] value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
