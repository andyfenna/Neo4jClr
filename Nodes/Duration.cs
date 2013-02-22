using System.Globalization;
using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public interface IDuration
    {
        int Value { get; set; }

        NodeReference Reference { get; set; }
    }
    
    public class Duration : IDuration
    {
        public int Value { get; set; }

        [JsonIgnore]
        public NodeReference Reference { get; set; }

    }
}