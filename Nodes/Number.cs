using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Number
    {
        [JsonProperty("Number")]
        public int Value { get; set; }

        [JsonIgnore]
        public Build Build { get; set; }

        [JsonIgnore]
        public NodeReference<Number> Reference { get; set; }
    }
}