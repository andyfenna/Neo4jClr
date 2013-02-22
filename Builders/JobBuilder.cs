using System.Linq;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class JobBuilder
    {
        private Job _job;

        public JobBuilder WithName(string jobName)
        {
            if(_job == null)
            {
                _job = new Job();
            }
            _job.Name = jobName;
            return this;
        }

        public JobBuilder WithStage(Stage stage)
        {
            if (_job == null)
            {
                _job = new Job();
            }
            _job.Stage = stage;
            return this;
        }

        private NodeReference<Job> GetJob()
        {
            var nJobNode = Client.Instance().RootNode.StartCypher("root")
                .Match("root-[:ROOT_HAS_PRODUCT]->" +
                       "product-[:PRODUCT_HAS_PIPELINE]->" +
                       "pipeline-[:PIPELINE_CONTAINS_STAGE]->" +
                       "stage-[:STAGE_GIVEN_JOB]->job")
                .Where("job.JobName = '" + _job.Name + "'")
                .Return<Node<Job>>("job")
                .Results.FirstOrDefault();

            return nJobNode != null ? nJobNode.Reference : null;
        }

        private NodeReference<Job> CreateJob()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("job")
                        {
                            {"name", _job.Name}
                        }
                };

            return Client.Instance().Create(
                _job,
                new IRelationshipAllowingParticipantNode<Job>[0],
                indexEntries);
        }

        public Job Build()
        {
            _job.Reference = GetJob() ?? CreateJob();

            if (!GetRelationship())
            {
                CreateRelationship();
            }

            return _job;
        }

        private bool GetRelationship()
        {
            return Client.Instance().RootNode.StartCypher("root")
                       .Match("root-[:ROOT_HAS_PRODUCT]->" +
                              "product-[:PRODUCT_HAS_PIPELINE]->" +
                              "pipeline-[:PIPELINE_CONTAINS_STAGE]->" +
                              "stage-[:STAGE_GIVEN_JOB]->job")
                       .Where("product.ProductName  = '" + _job.Stage.Pipeline.Product.Name + "'")
                       .And()
                       .Where("pipeline.PipelineName = '" + _job.Stage.Pipeline.Name + "'")
                       .And()
                       .Where("stage.StageName = '" + _job.Stage.Name + "'")
                       .And()
                       .Where("job.JobName = '" + _job.Name + "'")
                       .Return<Node<Job>>("job")
                       .Results.FirstOrDefault() != null;
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _job.Stage.Reference,
                new StageGivenJob(_job.Reference));
        }

        
    }
}