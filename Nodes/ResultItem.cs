using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class ResultItem : Result
    {
        [JsonIgnore]
        public NodeReference<ResultItem> Reference { get; set; }

        [JsonIgnore]
        public Build Build { get; set; }

        [JsonIgnore]
        public ResultItem MovedFrom { get; set; }

        [JsonIgnore]
        public string Type { get; set; }
    }
}