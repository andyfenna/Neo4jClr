using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Stage
    {
        [JsonProperty("StageName")]
        public string Name { get; set; }

        [JsonIgnore]
        public Pipeline Pipeline { get; set; }
        
        [JsonIgnore]
        public NodeReference<Stage> Reference { get; set; }
    }
}