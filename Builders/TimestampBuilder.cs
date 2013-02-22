using System;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    internal class TimestampBuilder
    {
        private Timestamp _timestamp;

        public TimestampBuilder WithTimestamp(DateTimeOffset timestamp)
        {
            _timestamp = new Timestamp {Value = timestamp};
            return this;
        }

        private NodeReference<Timestamp> CreateTimestamp()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("timestamp")
                        {
                            {"timestamp", _timestamp.Value}
                        }
                };

             return Client.Instance().Create(
                _timestamp,
                new IRelationshipAllowingParticipantNode<Timestamp>[0],
                indexEntries);
        }

        public Timestamp Build()
        {
            _timestamp.Reference = CreateTimestamp();
            return _timestamp;
        }
    }
}