using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    internal class StatusBuilder
    {
        private Status _status;

        public StatusBuilder WithStatus(string buildStatus)
        {
            if (_status == null)
            {
                _status = new Status();
            }
            _status.Value = buildStatus;
            return this;
        }

        public StatusBuilder WithBuild(Build build)
        {
            if (_status == null)
            {
                _status = new Status();
            }
            _status.Build = build;
            return this;
        }

        private NodeReference<Status> CreateStatus()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("buildStatus")
                        {
                            {"status", _status.Value}
                        }
                };

            return Client.Instance().Create(
                _status,
                new IRelationshipAllowingParticipantNode<Status>[0],
                indexEntries);
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _status.Build.Reference,
                new BuildProduced(_status.Reference, "status"));
        }

        public Status Build()
        {
            _status.Reference = CreateStatus();
            CreateRelationship();
            return _status;
        }
    }
}