using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Builders
{
    internal class DurationBuilder
    {
        private Duration _duration;

        public DurationBuilder WithDuration(int duration)
        {
            _duration = new Duration {Value = duration};
            return this;
        }

        private NodeReference<Duration> CreateDuration()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("duration")
                        {
                            {"duration", _duration.Value}
                        }
                };

            return Client.Instance().Create(
                _duration,
                new IRelationshipAllowingParticipantNode<Duration>[0],
                indexEntries);
        }

        public Duration Build()
        {
            _duration.Reference = CreateDuration();
            return _duration;
        }
    }
}