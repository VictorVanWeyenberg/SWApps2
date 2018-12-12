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
    public class RegisterWrapperJsonConverter : JsonConverter<RegisterWrapper>
    {
        public override RegisterWrapper ReadJson(JsonReader reader, Type objectType, RegisterWrapper existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException("Deze klasse wordt enkel gebruikt voor Serialization.");
        }

        public override void WriteJson(JsonWriter writer, RegisterWrapper wrapper, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(wrapper);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;

                o.Add(new JProperty("FirstName", wrapper.FirstName));
                o.Add(new JProperty("LastName", wrapper.LastName));
                o.Add(new JProperty("Email", wrapper.Email));
                o.Add(new JProperty("Hash", wrapper.Hash));
                o.Add(new JProperty("Salt", wrapper.Salt));
                o.Add(new JProperty("IsEntrepreneur", wrapper.IsEntrepreneur.ToString()));

                o.WriteTo(writer);
            }
        }
    }
}
