using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class AgentRunsBuild :
        Relationship,
        IRelationshipAllowingSourceNode<Agent>,
        IRelationshipAllowingTargetNode<Build>
    {
        public const string TypeKey = "AGENT_RUNS_BUILD";

        public AgentRunsBuild(NodeReference targetNode)
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