using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class BuildProduced :
        Relationship,
        IRelationshipAllowingSourceNode<Build>,
        IRelationshipAllowingTargetNode<Number>,
        IRelationshipAllowingTargetNode<Status>
    {
        private readonly string _keyType;

        public BuildProduced(NodeReference targetNode, string key): base(targetNode)
        {
            _keyType = string.Format("BUILD_PRODUCED_{0}", key.ToUpper());
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return _keyType; }
        }
    }
}