using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class ProductHasPipeline :
        Relationship,
        IRelationshipAllowingSourceNode<Product>,
        IRelationshipAllowingTargetNode<Pipeline>
    {
        public const string TypeKey = "PRODUCT_HAS_PIPELINE";

        public ProductHasPipeline(NodeReference targetNode)
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