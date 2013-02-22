using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class BuildStartedAt :
        Relationship,
        IRelationshipAllowingSourceNode<Build>,
        IRelationshipAllowingTargetNode<Timestamp>
    {
        public const string TypeKey = "BUILD_STARTED_AT";

        public BuildStartedAt(NodeReference targetNode)
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