using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Pipeline
    {
        [JsonProperty("PipelineName")]
        public string Name { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [JsonIgnore]
        public NodeReference<Pipeline> Reference { get; set; }
    }
}