using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class ResultItemStartedAt :
        Relationship,
        IRelationshipAllowingSourceNode<ResultItem>,
        IRelationshipAllowingTargetNode<Timestamp>
    {
        private readonly string _keyType;

        public ResultItemStartedAt(NodeReference targetNode, string key) : base(targetNode)
        {
            _keyType = string.Format("{0}_AT", key.ToUpper());
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return _keyType; }
        }
    }
}