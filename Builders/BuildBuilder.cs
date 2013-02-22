using System;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class BuildBuilder
    {
        private Build _build;

        public BuildBuilder WithNumber(int buildNumber)
        {
            if(_build == null)
            {
                _build = new Build();
            }
            _build.Number = buildNumber;
            return this;
        }

        public BuildBuilder WithTimestamp(DateTimeOffset timestamp)
        {
            if (_build == null)
            {
                _build = new Build();
            }
            _build.Timestamp = new TimestampBuilder()
                .WithTimestamp(timestamp)
                .Build();
            return this;
        }

        public BuildBuilder WithDuration(int duration)
        {
            if (_build == null)
            {
                _build = new Build();
            }
            _build.Duration = new DurationBuilder()
                .WithDuration(duration)
                .Build();
            return this;
        }

        public BuildBuilder WithStatus(string buildStatus)
        {
            if (_build == null)
            {
                _build = new Build();
            }
            _build.Status = buildStatus;
            return this;
        }

        public BuildBuilder WithAgent(Agent agent)
        {
            if (_build == null)
            {
                _build = new Build();
            }
            _build.Agent = agent;
            return this;
        }

        private NodeReference<Build> CreateBuild()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("build")
                        {
                            {"number", _build.Number},
                            {"status", _build.Status},
                            {"duration", _build.Duration.Value},
                            {"timestamp", _build.Timestamp.Value}
                        }
                };

            return Client.Instance().Create(
                _build,
                new IRelationshipAllowingParticipantNode<Build>[0],
                indexEntries);
        }

        public Build Build()
        {
            _build.Reference = CreateBuild();

            CreateRelationships();

            return _build;
        }

        private void CreateRelationships()
        {
            new NumberBuilder().WithNumber(_build.Number).WithBuild(_build).Build();
            new StatusBuilder().WithStatus(_build.Status).WithBuild(_build).Build();

            var timestamp = new TimestampBuilder().WithTimestamp(_build.Timestamp.Value).Build();
            var duration = new DurationBuilder().WithDuration(_build.Duration.Value).Build();

            CreateTimestampRelationship(timestamp);
            CreateDurationRelationship(duration);
            CreateRelationship();
        }

        private void CreateTimestampRelationship(Timestamp timestamp)
        {
            Client.Instance().CreateRelationship(
                _build.Reference,
                new BuildStartedAt(timestamp.Reference));
        }

        private void CreateDurationRelationship(Duration duration)
        {
            Client.Instance().CreateRelationship(
                _build.Reference,
                new BuildTook(duration.Reference));
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _build.Agent.Reference,
                new AgentRunsBuild(_build.Reference));
        }
    }
}