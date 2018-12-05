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
    class ServiceHoursJsonConverter : JsonConverter<ServiceHours>
    {
        public override ServiceHours ReadJson(JsonReader reader, Type objectType, ServiceHours existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            ServiceHours sh = new ServiceHours();
            JArray jsonsh = jObject.Value<JArray>("ServiceHours");
            foreach (JToken jsonTimeInterval in jsonsh)
            {
                int day = jsonTimeInterval.Value<int>("DayOfWeek");
                NodaTime.LocalTime startTime = new NodaTime.LocalTime((jsonTimeInterval as dynamic).Value<int>("StartHour"), (jsonTimeInterval as dynamic).Value<int>("StartMinute"));
                NodaTime.LocalTime endTime = new NodaTime.LocalTime((jsonTimeInterval as dynamic).Value<int>("EndHour"), (jsonTimeInterval as dynamic).Value<int>("EndMinute"));
                TimeInterval ti = new TimeInterval(startTime, endTime);
                sh.setHoursForDay(day, ti);
            }
            return sh;
        }

        public override void WriteJson(JsonWriter writer, ServiceHours value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}
