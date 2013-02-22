using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Job
    {
        [JsonProperty("JobName")]
        public string Name { get; set; }

        [JsonIgnore]
        public Stage Stage { get; set; }

        [JsonIgnore]
        public NodeReference<Job> Reference { get; set; }
    }
}