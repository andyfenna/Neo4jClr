using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class StageGivenJob :
        Relationship,
        IRelationshipAllowingSourceNode<Stage>,
        IRelationshipAllowingTargetNode<Job>
    {
        public const string TypeKey = "STAGE_GIVEN_JOB";

        public StageGivenJob(NodeReference targetNode)
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