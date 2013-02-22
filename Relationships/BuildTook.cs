using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class BuildTook :
        Relationship,
        IRelationshipAllowingSourceNode<Build>,
        IRelationshipAllowingTargetNode<Duration>
    {
        public const string TypeKey = "BUILD_TOOK";

        public BuildTook(NodeReference targetNode)
            : base(targetNode)
        {
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}