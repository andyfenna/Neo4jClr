using System.Linq;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class AgentBuilder
    {
        private Agent _agent;

        public AgentBuilder WithName (string agentName)
        {
            if(_agent == null)
            {
                _agent = new Agent();
            }
            _agent.Name = agentName;
            return this;
        }

        public AgentBuilder WithJob(Job job)
        {
            if (_agent == null)
            {
                _agent = new Agent();
            }
            _agent.Job = job;
            return this;
        }

        private NodeReference<Agent> GetAgent()
        {
            var nAgentNode = Client.Instance().RootNode.StartCypher("root")
                .Match("root-[:ROOT_HAS_PRODUCT]->" +
                        "product-[:PRODUCT_HAS_PIPELINE]->" +
                        "pipeline-[:PIPELINE_CONTAINS_STAGE]->" +
                        "stage-[:STAGE_GIVEN_JOB]->" +
                        "job-[:JOB_ASSIGNED_AGENT]->" +
                        "agent")
                .Where("agent.AgentName = '" + _agent.Name + "'")
                .Return<Node<Agent>>("agent")
                .Results.FirstOrDefault();

            return nAgentNode != null ? nAgentNode.Reference : null;
        }

        private NodeReference<Agent> CreateAgent()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("agent")
                        {
                            {"name", _agent.Name}
                        }
                };

            return Client.Instance().Create(
                _agent,
                new IRelationshipAllowingParticipantNode<Agent>[0],
                indexEntries);
        }

        public Agent Build()
        {
            _agent.Reference = GetAgent() ?? CreateAgent();

            if (!GetRelationship())
            {
                CreateRelationship();
            }

            return _agent;
        }

        private bool GetRelationship()
        {
            return Client.Instance().RootNode.StartCypher("root")
                       .Match("root-[:ROOT_HAS_PRODUCT]->" +
                              "product-[:PRODUCT_HAS_PIPELINE]->" +
                              "pipeline-[:PIPELINE_CONTAINS_STAGE]->" +
                              "stage-[:STAGE_GIVEN_JOB]->" +
                              "job-[:JOB_ASSIGNED_AGENT]->" +
                              "agent")
                       .Where("product.ProductName  = '" + _agent.Job.Stage.Pipeline.Product.Name + "'")
                       .And()
                       .Where("pipeline.PipelineName = '" + _agent.Job.Stage.Pipeline.Name + "'")
                       .And()
                       .Where("stage.StageName = '" + _agent.Job.Stage.Name + "'")
                       .And()
                       .Where("job.JobName = '" + _agent.Job.Name + "'")
                       .And()
                       .Where("agent.AgentName = '" + _agent.Name + "'")
                       .Return<Node<Agent>>("agent")
                       .Results.FirstOrDefault() != null;
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _agent.Job.Reference,
                new JobAssignedAgent(_agent.Reference));
        }
    }
}