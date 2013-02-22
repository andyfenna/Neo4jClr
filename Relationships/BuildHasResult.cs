using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class BuildHasResult :
        Relationship,
        IRelationshipAllowingSourceNode<Build>,
        IRelationshipAllowingSourceNode<Result>
    {
        public const string TypeKey = "BUILD_HAS_RESULT";

        public BuildHasResult(NodeReference targetNode)
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