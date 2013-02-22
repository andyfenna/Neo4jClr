using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class ResultItemTook :
        Relationship,
        IRelationshipAllowingSourceNode<ResultItem>,
        IRelationshipAllowingSourceNode<Duration>
    {
        private readonly string _keyType;

        public ResultItemTook(NodeReference targetNode, string key) : base(targetNode)
        {
            _keyType = string.Format("{0}_TOOK", key.ToUpper());
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return _keyType; }
        }
    }
}