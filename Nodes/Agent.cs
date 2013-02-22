using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Agent
    {
        [JsonProperty("AgentName")]
        public string Name { get; set; }

        [JsonIgnore]
        public Job Job { get; set; }
        
        [JsonIgnore]
        public NodeReference<Agent> Reference { get; set; }
    }
}