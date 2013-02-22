using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class Product
    {
        [JsonProperty("ProductName")]
        public string Name { get; set; }

        [JsonIgnore]
        public Pipeline Pipeline { get; set; }

        [JsonIgnore]
        public NodeReference<Product> Reference { get; set; }
    }
}