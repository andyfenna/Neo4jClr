using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class BuildWas :
        Relationship,
        IRelationshipAllowingSourceNode<Build>,
        IRelationshipAllowingTargetNode<ResultItem>
    {
        private readonly string _keyType;

        public BuildWas(NodeReference targetNode, string key) : base(targetNode)
        {
            _keyType = string.Format("BUILD_WAS_{0}", key.ToUpper());
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return _keyType; }
        }
    }
}