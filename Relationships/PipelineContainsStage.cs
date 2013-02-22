using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class PipelineContainsStage :
        Relationship,
        IRelationshipAllowingSourceNode<Pipeline>,
        IRelationshipAllowingTargetNode<Stage>
    {
        public const string TypeKey = "PIPELINE_CONTAINS_STAGE";

        public PipelineContainsStage(NodeReference targetNode)
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