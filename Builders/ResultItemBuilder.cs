using System;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class ResultItemBuilder
    {
        private ResultItem _item;

        public ResultItemBuilder WithBuild(Build build)
        {
            if (_item == null)
            {
                _item = new ResultItem();
            }
            _item.Build = build;
            return this;
        }

        public ResultItemBuilder WithType(string type)
        {
            if (_item == null)
            {
                _item = new ResultItem();
            }
            _item.Type = type;
            return this;
        }

        public ResultItemBuilder WithTimestamp(DateTimeOffset timestamp)
        {
            if (_item == null)
            {
                _item = new ResultItem();
            }

            _item.Timestamp = new TimestampBuilder()
                .WithTimestamp(timestamp)
                .Build();

            return this;
        }

        public ResultItemBuilder WithDuration(int duration)
        {
            if (_item == null)
            {
                _item = new ResultItem();
            }

            _item.Duration = new DurationBuilder()
                .WithDuration(duration)
                .Build();

            return this;
        
        }
        
        public ResultItemBuilder WithMovedFrom(ResultItem completing)
        {
            if (_item == null)
            {
                _item = new ResultItem();
            }
            _item.MovedFrom = completing;
            return this;
        }

        private void CreateItem()
        {
            var duration = 0;

            if (_item.Duration != null)
            {
                duration = _item.Duration.Value;
            }

            var timestamp = new DateTimeOffset();

            if (_item.Timestamp != null)
            {
                timestamp = _item.Timestamp.Value;
            }

            var indexEntries = new[]
                {
                    new IndexEntry(_item.Type)
                        {
                            {"timestamp", timestamp},
                            {"durationt", duration}
                        }
                };

            _item.Reference = Client.Instance().Create(
                _item,
                new IRelationshipAllowingParticipantNode<ResultItem>[0],
                indexEntries);
        }

        public ResultItem Build()
        {
            CreateItem();

            CreateRelationships();

            return _item;
        }

        private void CreateRelationships()
        {
            CreateRelationship();

            if (_item.Duration != null) CreateDurationRelationship();

            if (_item.Reference != null) CreateTimestampRelationship();

            if (_item.MovedFrom != null) CreateMovededToRelationship();

        }

        private void CreateMovededToRelationship()
        {
            Client.Instance().CreateRelationship(
               _item.MovedFrom.Reference,
               new ResultItemMovedTo(_item, _item.MovedFrom.Type));
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _item.Build.Reference,
                new BuildWas(_item.Reference, _item.Type));
        }

        private void CreateDurationRelationship()
        {
            Client.Instance().CreateRelationship(
                _item.Reference,
                new ResultItemTook(_item.Duration.Reference, _item.Type));
        }

        private void CreateTimestampRelationship()
        {
            Client.Instance().CreateRelationship(
                _item.Reference,
                new ResultItemStartedAt(_item.Timestamp.Reference, _item.Type));
        }

        
    }
}