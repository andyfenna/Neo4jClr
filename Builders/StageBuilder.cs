using System.Linq;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class StageBuilder
    {
        private Stage _stage;

        public StageBuilder WithName(string stageName)
        {
            if(_stage == null)
            {
                _stage = new Stage();
            }

            _stage.Name = stageName;

            return this;
        }

        public StageBuilder WithPipeline(Pipeline pipeline)
        {
            if (_stage == null)
            {
                _stage = new Stage();
            }

            _stage.Pipeline = pipeline;

            return this;
        }

        private NodeReference<Stage> GetStage()
        {
            var nStageNode = Client.Instance().RootNode.StartCypher("root")
               .Match("root-[:ROOT_HAS_PRODUCT]->product-[:PRODUCT_HAS_PIPELINE]->pipeline-[:PIPELINE_CONTAINS_STAGE]->stage")
               .Where("stage.StageName = '" + _stage.Name + "'")
               .Return<Node<Stage>>("stage")
               .Results.FirstOrDefault();

            return nStageNode != null ? nStageNode.Reference : null;
        }

        private NodeReference<Stage> CreateStage()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("stage")
                        {
                            {"name", _stage.Name}
                        }
                };

            return Client.Instance().Create(
                _stage,
                new IRelationshipAllowingParticipantNode<Stage>[0],
                indexEntries);
        }

        public Stage Build()
        {
            _stage.Reference = GetStage() ?? CreateStage();

            if (!GetRelationship())
            {
                CreateRelationship();
            }

            return _stage;
        }

        private bool GetRelationship()
        {
            return Client.Instance().RootNode.StartCypher("root")
                       .Match("root-[:ROOT_HAS_PRODUCT]->product-[:PRODUCT_HAS_PIPELINE]->pipeline-[:PIPELINE_CONTAINS_STAGE]->stage")
                       .Where("product.ProductName  = '" + _stage.Pipeline.Product.Name + "'")
                       .And()
                       .Where("pipeline.PipelineName = '" + _stage.Pipeline.Name + "'")
                       .And()
                       .Where("stage.StageName = '" + _stage.Name + "'")
                       .Return<Node<Stage>>("stage")
                       .Results.FirstOrDefault() != null;
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _stage.Pipeline.Reference,
                new PipelineContainsStage(_stage.Reference));
        }
    }
}