using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Status
    {
        [JsonProperty("Status")]
        public string Value { get; set; }

        [JsonIgnore]
        public Build Build { get; set; }

        [JsonIgnore]
        public NodeReference<Status> Reference { get; set; }
    }
}