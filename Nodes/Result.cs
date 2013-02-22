using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    [JsonObject]
    public class Result 
    {
        [JsonProperty("Duration")]
        [JsonConverter(typeof(ConcreateTypeConverter<IDuration>))]
        public Duration Duration { get; set; }

        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(ConcreateTypeConverter<ITimestamp>))]
        public Timestamp Timestamp { get; set; }
    }
}
