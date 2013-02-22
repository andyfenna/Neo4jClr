using System;
using Neo4jClient;
using Newtonsoft.Json;

namespace Neo4jClr.Nodes
{
    public class ResultOld
    {
        [JsonIgnore]
        public ResultItem Scheduling { set; get; }

        [JsonIgnore]
        public ResultItem Assigning { set; get; }

        [JsonIgnore]
        public ResultItem Preparing { set; get; }

        [JsonIgnore]
        public ResultItem Building { set; get; }

        [JsonIgnore]
        public ResultItem Completing { set; get; }

        [JsonIgnore]
        public ResultItem Completed { set; get; }

        [JsonProperty("SchedulingTimestamp")]
        public DateTimeOffset SchedulingTimestamp
        {
            get { return Scheduling.Timestamp.Value; }
            set
            {
                if(Scheduling == null)
                {
                    Scheduling = new ResultItem();
                }
                Scheduling.Timestamp = new Timestamp {Value = value};
            }

        }

        [JsonProperty("SchedulingDuration")]
        public int SchedulingDuration
        {
            get { return Scheduling.Duration.Value; }
            set
            {
                if (Scheduling == null)
                {
                    Scheduling = new ResultItem();
                }
                //Scheduling.Duration = new Duration{ Value = value };
            }
        }

        [JsonProperty("AssigningTimestamp")]
        public DateTimeOffset AssigningTimestamp
        {
            get { return Assigning.Timestamp.Value; }
            set
            {
                if (Assigning == null)
                {
                    Assigning = new ResultItem();
                }
                Assigning.Timestamp = new Timestamp { Value = value };
            }
        }

        [JsonProperty("AssigningDuration")]
        public int AssigningDuration
        {
            get { return Assigning.Duration.Value; }
            set
            {
                if (Assigning == null)
                {
                    Assigning = new ResultItem();
                }
                Assigning.Duration = new Duration { Value = value };
            }
        }

        [JsonProperty("PreparingTimestamp")]
        public DateTimeOffset PreparingTimestamp
        {
            get { return Preparing.Timestamp.Value; }
            set
            {
                if (Preparing == null)
                {
                    Preparing = new ResultItem();
                }
                Preparing.Timestamp = new Timestamp { Value = value };
            }
        }

        [JsonProperty("PreparingDuration")]
        public int PreparingDuration
        {
            get { return Preparing.Duration.Value; }
            set
            {
                if (Preparing == null)
                {
                    Preparing = new ResultItem();
                }
                Preparing.Duration = new Duration { Value = value };
            }
        }

        [JsonProperty("BuildingTimestamp")]
        public DateTimeOffset BuildingTimestamp
        {
            get { return Building.Timestamp.Value; }
            set
            {
                if (Building == null)
                {
                    Building = new ResultItem();
                }
                Building.Timestamp = new Timestamp { Value = value };
            }
        }

        [JsonProperty("BuildingDuration")]
        public int BuildingDuration
        {
            get { return Building.Duration.Value; }
            set
            {
                if (Building == null)
                {
                    Building = new ResultItem();
                }
                Building.Duration = new Duration { Value = value };
            }
        }

        [JsonProperty("CompletingTimestamp")]
        public DateTimeOffset CompletingTimestamp
        {
            get { return Completing.Timestamp.Value; }
            set
            {
                if (Completing == null)
                {
                    Completing = new ResultItem();
                }
                Completing.Timestamp = new Timestamp { Value = value };
            }
        }

        [JsonProperty("CompletingDuration")]
        public int CompletingDuration
        {
            get { return Completing.Duration.Value; }
            set
            {
                if (Completing == null)
                {
                    Completing = new ResultItem();
                }
                Completing.Duration = new Duration { Value = value };
            }
        }

        [JsonProperty("CompletedTimestamp")]
        public DateTimeOffset CompletedTimestamp
        {
            get { return Completed.Timestamp.Value; }
            set
            {
                if (Completed == null)
                {
                    Completed = new ResultItem();
                }
                Completed.Timestamp = new Timestamp { Value = value };
            }
        }

        [JsonIgnore]
        public Build Build { get; set; }

        [JsonIgnore]
        public NodeReference<ResultOld> Reference { get; set; }
    }
}