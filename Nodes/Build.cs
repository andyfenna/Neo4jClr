using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    [JsonObject]
    public class Build : Result
    {
        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
        
        [JsonIgnore]
        public Agent Agent { get; set; }

        [JsonIgnore]
        public NodeReference<Build> Reference { get; set; }
    }
}