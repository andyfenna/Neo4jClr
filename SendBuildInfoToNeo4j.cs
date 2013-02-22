using Neo4jClr.Builders;
using Neo4jClr.Nodes;

namespace Neo4jClr
{
    public class SendBuildInfo
    {
        private readonly AgentInfo _agentInfo;
        private readonly BuildInfo _buildInfo;
        private readonly ResultInfo _resultInfo;

        public SendBuildInfo(string url, AgentInfo agentInfo, BuildInfo buildInfo, ResultInfo resultInfo)
        {
            Client.Url = url;
            _agentInfo = agentInfo;
            _buildInfo = buildInfo;
            _resultInfo = resultInfo;
        }

        public int Send()
        {
            var product = CreateProduct();

            var pipeline = CreatePipeline(product);

            var stage = CreateStage(pipeline);

            var job = CreateJob(stage);

            var agent = CreateAgent(job);

            var build = CreateBuild(agent);

            return CreateResults(build);
        }

        private Job CreateJob(Stage stage)
        {
            return new JobBuilder()
                .WithName(_agentInfo.JobName)
                .WithStage(stage)
                .Build();
        }

        private Product CreateProduct()
        {
            return new ProductBuilder()
                .WithName(_agentInfo.ProductName)
                .Build();
        }

        private Pipeline CreatePipeline(Product product)
        {
            return new PipelineBuilder()
                .WithName(_agentInfo.PipelineName)
                .WithProduct(product)
                .Build();
        }

        private Stage CreateStage(Pipeline pipeline)
        {
            return new StageBuilder()
                .WithName(_agentInfo.StageName)
                .WithPipeline(pipeline)
                .Build();
        }

        private Agent CreateAgent(Job job)
        {
            return new AgentBuilder()
                .WithName(_agentInfo.AgentName)
                .WithJob(job)
                .Build();
        }

        private Build CreateBuild(Agent agent)
        {
            return new BuildBuilder()
                .WithAgent(agent)
                .WithNumber(_buildInfo.Number)
                .WithTimestamp(_buildInfo.Timestamp)
                .WithDuration(_buildInfo.Duration)
                .WithStatus(_buildInfo.Status)
                .Build();
        }

        private int CreateResults(Build build)
        {
            var scheduled = new ResultItemBuilder()
                .WithBuild(build)
                .WithType("scheduled")
                .WithTimestamp(_resultInfo.SchedulingTimestamp)
                .WithDuration(_resultInfo.SchedulingDuration)
                .Build();

            var assigned = new ResultItemBuilder()
                .WithBuild(build)
                .WithType("assigned")
                .WithTimestamp(_resultInfo.AssigningTimestamp)
                .WithDuration(_resultInfo.AssigningDuration)
                .WithMovedFrom(scheduled)
                .Build();

            var prepared = new ResultItemBuilder()
                 .WithBuild(build)
                 .WithType("prepared")
                 .WithTimestamp(_resultInfo.PreparingTimestamp)
                 .WithDuration(_resultInfo.PreparingDuration)
                 .WithMovedFrom(assigned)
                 .Build();

            var built = new ResultItemBuilder()
                 .WithBuild(build)
                 .WithType("built")
                 .WithTimestamp(_resultInfo.BuildingTimestamp)
                 .WithDuration(_resultInfo.BuildingDuration)
                 .WithMovedFrom(prepared)
                 .Build();

            var completing = new ResultItemBuilder()
                 .WithBuild(build)
                 .WithType("completing")
                 .WithTimestamp(_resultInfo.CompletingTimestamp)
                 .WithDuration(_resultInfo.CompletingDuration)
                 .WithMovedFrom(built)
                 .Build();

            new ResultItemBuilder()
                .WithBuild(build)
                .WithType("completed")
                .WithTimestamp(_resultInfo.CompletedTimestamp)
                .WithMovedFrom(completing)
                .Build();
            return 1;
        }
    }
}