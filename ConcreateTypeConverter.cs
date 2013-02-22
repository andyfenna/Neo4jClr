using Neo4jClr.Nodes;
using Newtonsoft.Json;
using System;

namespace Neo4jClr
{
    internal class ConcreateTypeConverter<TConcrete> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //assume we can convert to anything for now
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            //explicitly specify the concrete type we want to create
            return serializer.Deserialize<TConcrete>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine
            serializer.Converters.Clear();

            if (value is Duration)
                serializer.Serialize(writer, ((Duration)value).Value);
            else
                serializer.Serialize(writer, value.ToString());
        }
    }
}
