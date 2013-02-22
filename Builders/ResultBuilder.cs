using System;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class ResultBuilder
    {
        private Result _result;

        public ResultBuilder WithBuild(Build build)
        {
            if(_result == null)
            {
                _result = new ResultOld();
            }

            _result.Build = build;
            return this;
        }

        public ResultBuilder WithScheduling(DateTime schedTimestamp, int schedDuration)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Scheduling = new ResultItemBuilder()
                .WithType("scheduling")
                .WithTimestamp(schedTimestamp)
                .WithDuration(schedDuration)
                .Build();

            return this;
        }


        public ResultBuilder WithAssigning(DateTime assTimestamp, int assDuration)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Assigning = new ResultItemBuilder()
                .WithType("assigning")
                .WithTimestamp(assTimestamp)
                .WithDuration(assDuration)
                .Build();

            return this;
        }


        public ResultBuilder WithPreparing(DateTime prepTimestamp, int prepDuration)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Preparing = new ResultItemBuilder()
                .WithType("preparing")
                .WithTimestamp(prepTimestamp)
                .WithDuration(prepDuration)
                .Build();

            return this;
        }

        public ResultBuilder WithBuilding(DateTime buildTimestamp, int buildDuration)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Building = new ResultItemBuilder()
                    .WithType("building")
                    .WithTimestamp(buildTimestamp)
                    .WithDuration(buildDuration)
                    .Build();

            return this;
        }

        public ResultBuilder WithCompleting(DateTime completingTimestamp, int completingDuration)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Completing = new ResultItemBuilder()
                .WithType("completing")
                .WithTimestamp(completingTimestamp)
                .WithDuration(completingDuration)
                .Build();

            return this;
        }

        public ResultBuilder WithCompleted(DateTime completedTimestamp)
        {
            if (_result == null)
            {
                _result = new ResultOld();
            }

            _result.Completed = new ResultItemBuilder()
                .WithType("completed")
                .WithTimestamp(completedTimestamp)
                .Build();

            return this;
        }

        private NodeReference<ResultOld> CreateResult()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("result")
                        {
                            {"schedulingTimestamp", _result.SchedulingTimestamp},
                            {"schedulingDuration", _result.SchedulingDuration},
                            {"assigningTimestamp", _result.AssigningTimestamp},
                            {"assigningDuration", _result.AssigningDuration},
                            {"preparingTimestamp", _result.PreparingTimestamp},
                            {"preparingDuration", _result.PreparingDuration},
                            {"buildingTimestamp", _result.BuildingTimestamp},
                            {"buildingDuration", _result.BuildingDuration},
                            {"completingTimestamp", _result.CompletingTimestamp},
                            {"completingDuration", _result.CompletingDuration},
                            {"completedTimestamp", _result.CompletedTimestamp},
                        }
                };

            var result = Client.Instance().Create(
                _result,
                new IRelationshipAllowingParticipantNode<ResultOld>[0],
                indexEntries);

            return result;
        }

        private void CreateRelationships()
        {
            CreateRelationship();
            CreateRelationship(_result.Scheduling);
            CreateRelationship(_result.Assigning);
            CreateRelationship(_result.Preparing);
            CreateRelationship(_result.Building);
            CreateRelationship(_result.Completing);
            CreateRelationship(_result.Completed);
        }

        public ResultOld Build()
        {
            _result.Reference = CreateResult();

            CreateRelationships();

            return _result;
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _result.Build.Reference,
                new BuildHasResult(_result.Reference));
        }

        private void CreateRelationship(ResultItem item)
        {
            Client.Instance().CreateRelationship(
                item.Reference,
                new ResultItemPartOfResult(_result.Reference, item.Type));
        }
    }
}