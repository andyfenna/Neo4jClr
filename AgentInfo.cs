namespace Neo4jClr
{
    public class AgentInfo
    {
        private readonly string _agentName;
        private readonly string _jobName;
        private readonly string _pipelineName;
        private readonly string _productName;
        private readonly string _stageName;

        public AgentInfo(string productName, string pipelineName, string stageName, string jobName, string agentName)
        {
            _productName = productName;
            _pipelineName = pipelineName;
            _stageName = stageName;
            _jobName = jobName;
            _agentName = agentName;
        }

        public string ProductName
        {
            get { return _productName; }
        }

        public string PipelineName
        {
            get { return _pipelineName; }
        }

        public string StageName
        {
            get { return _stageName; }
        }

        public string JobName
        {
            get { return _jobName; }
        }

        public string AgentName
        {
            get { return _agentName; }
        }
    }
}