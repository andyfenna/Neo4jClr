using System;
using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public interface ITimestamp
    {
        DateTimeOffset Value { get; set; }
        NodeReference Reference { get; set; }
    }

    public class Timestamp : ITimestamp
    {
        public DateTimeOffset Value { get; set; }

        [JsonIgnore]
        public NodeReference Reference { get; set; }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
        }
    }
}