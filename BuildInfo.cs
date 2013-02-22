using System;

namespace Neo4jClr
{
    public class BuildInfo
    {
        private readonly int _duration;
        private readonly int _number;
        private readonly string _status;
        private readonly DateTime _timestamp;

        public BuildInfo(string status, int number, int duration, DateTime timestamp)
        {
            _status = status;
            _number = number;
            _duration = duration;
            _timestamp = timestamp;
        }

        public string Status
        {
            get { return _status; }
        }

        public int Number
        {
            get { return _number; }
        }

        public int Duration
        {
            get { return _duration; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
        }
    }
}