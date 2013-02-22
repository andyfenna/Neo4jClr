using System.Linq;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class PipelineBuilder
    {
        private Pipeline _pipeline;

        public PipelineBuilder WithName(string pipelineName)
        {
            if (_pipeline == null)
            {
                _pipeline = new Pipeline();
            }

            _pipeline.Name = pipelineName;
            return this;
        }

        public PipelineBuilder WithProduct(Product product)
        {
            if (_pipeline == null)
            {
                _pipeline = new Pipeline();
            }

            _pipeline.Product = product;

            return this;
        }

        private NodeReference<Pipeline> GetPipeline()
        {
            var pipelineNode = Client.Instance().RootNode.StartCypher("root")
               .Match("root-[:ROOT_HAS_PRODUCT]->product-[:PRODUCT_HAS_PIPELINE]->pipeline")
               .Where("product.ProductName  = '" + _pipeline.Product.Name + "'")
               .And()
               .Where("pipeline.PipelineName = '" + _pipeline.Name + "'")
               .Return<Node<Pipeline>>("pipeline")
               .Results.FirstOrDefault();

            return pipelineNode != null ? pipelineNode.Reference : null;
        }

        private NodeReference<Pipeline> CreatePipeline()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("pipeline")
                        {
                            {"name", _pipeline.Name}
                        }
                };

            return Client.Instance().Create(
                _pipeline,
                new IRelationshipAllowingParticipantNode<Pipeline>[0],
                indexEntries);
        }

        public Pipeline Build()
        {
            _pipeline.Reference = GetPipeline();

            if (_pipeline.Reference == null)
            {
                _pipeline.Reference = CreatePipeline();
                CreateRelationship();
            }
            
            return _pipeline;
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _pipeline.Product.Reference,
                new ProductHasPipeline(_pipeline.Reference));
        }
    }
}