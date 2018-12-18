using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swapps_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Converters
{
    public class TimeIntervalJsonConverter : JsonConverter<TimeInterval>
    {
        private static int dayOfWeek = -1;
        public override TimeInterval ReadJson(JsonReader reader, Type objectType, TimeInterval existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            int startHour = jObject.Value<JObject>("Start").Value<int>("Hour");
            int startMinute = jObject.Value<JObject>("Start").Value<int>("Minute");
            int endHour = jObject.Value<JObject>("End").Value<int>("Hour");
            int endMinute = jObject.Value<JObject>("End").Value<int>("Minute");
            dayOfWeek++;
            if (dayOfWeek == 7)
            {
                dayOfWeek = 0;
            }
            return new TimeInterval
            {
                DayOfWeek = dayOfWeek,
                StartHour = startHour,
                StartMinute = startMinute,
                EndHour = endHour,
                EndMinute = endMinute
            };
        }

        public override void WriteJson(JsonWriter writer, TimeInterval value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}