using System;

namespace Neo4jClr
{
    public class ResultInfo
    {
        private readonly int _assigningDuration;
        private readonly DateTime _assigningTimestamp;
        private readonly int _buildingDuration;
        private readonly DateTime _buildingTimestamp;
        private readonly DateTime _completedTimestamp;
        private readonly int _completingDuration;
        private readonly DateTime _completingTimestamp;
        private readonly int _preparingDuration;
        private readonly DateTime _preparingTimestamp;
        private readonly int _schedulingDuration;
        private readonly DateTime _schedulingTimestamp;

        public ResultInfo(DateTime schedulingTimestamp, int schedulingDuration, DateTime assigningTimestamp,
                          int assigningDuration, DateTime preparingTimestamp, int preparingDuration,
                          DateTime buildingTimestamp, int buildingDuration, DateTime completingTimestamp,
                          int completingDuration, DateTime completedTimestamp)
        {
            _schedulingTimestamp = schedulingTimestamp;
            _schedulingDuration = schedulingDuration;
            _assigningTimestamp = assigningTimestamp;
            _assigningDuration = assigningDuration;
            _preparingTimestamp = preparingTimestamp;
            _preparingDuration = preparingDuration;
            _buildingTimestamp = buildingTimestamp;
            _buildingDuration = buildingDuration;
            _completingTimestamp = completingTimestamp;
            _completingDuration = completingDuration;
            _completedTimestamp = completedTimestamp;
        }

        public DateTime SchedulingTimestamp
        {
            get { return _schedulingTimestamp; }
        }

        public int SchedulingDuration
        {
            get { return _schedulingDuration; }
        }

        public DateTime AssigningTimestamp
        {
            get { return _assigningTimestamp; }
        }

        public int AssigningDuration
        {
            get { return _assigningDuration; }
        }

        public DateTime PreparingTimestamp
        {
            get { return _preparingTimestamp; }
        }

        public int PreparingDuration
        {
            get { return _preparingDuration; }
        }

        public DateTime BuildingTimestamp
        {
            get { return _buildingTimestamp; }
        }

        public int BuildingDuration
        {
            get { return _buildingDuration; }
        }

        public DateTime CompletingTimestamp
        {
            get { return _completingTimestamp; }
        }

        public int CompletingDuration
        {
            get { return _completingDuration; }
        }

        public DateTime CompletedTimestamp
        {
            get { return _completedTimestamp; }
        }
    }
}