﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Converters
{
    public class EstablishmentEventJsonConverter : JsonConverter<EstablishmentEvent>
    {
        public override EstablishmentEvent ReadJson(JsonReader reader, Type objectType, EstablishmentEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            string name = jObject.Value<string>("Name");
            string description = jObject.Value<string>("Description");
            DateTime startDate = jObject.Value<DateTime>("StartDate");
            DateTime endDate = jObject.Value<DateTime>("EndDate");
            return new EstablishmentEvent(null, name, description, startDate, endDate);
        }

        public override void WriteJson(JsonWriter writer, EstablishmentEvent value, JsonSerializer serializer)
        {
            throw new NotSupportedException("JSON converter wordt enkel gebruikt voor deserialization.");
        }
    }
}