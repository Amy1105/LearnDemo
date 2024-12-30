using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.JsonConvertLearn
{
    public class SubItemConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(List<SubItem>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            // 假设JSON中的子集合以"subItems"键值对的形式出现
            JObject jsonObject = JObject.Load(reader);
            JToken subItemsToken = jsonObject["subItems"];

            if (subItemsToken == null)
            {
                return null;
            }

            using (JsonReader subItemsReader = subItemsToken.CreateReader())
            {
                return serializer.Deserialize<List<SubItem>>(subItemsReader);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
