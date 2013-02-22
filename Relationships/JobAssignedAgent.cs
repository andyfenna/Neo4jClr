using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class JobAssignedAgent :
        Relationship,
        IRelationshipAllowingSourceNode<Job>,
        IRelationshipAllowingTargetNode<Agent>
    {
        public const string TypeKey = "JOB_ASSIGNED_AGENT";

        public JobAssignedAgent(NodeReference targetNode)
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