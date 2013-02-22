using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    internal class NumberBuilder
    {
        private Number _number;

        public NumberBuilder WithNumber(int buildNumber)
        {
            if(_number == null)
            {
                _number = new Number();
            }
            _number.Value = buildNumber;
            return this;
        }

        public NumberBuilder WithBuild(Build build)
        {
            if (_number == null)
            {
                _number = new Number();
            }
            _number.Build = build;
            return this;
        }
        
        private NodeReference<Number> CreateNumber()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("buildNumber")
                        {
                            {"number", _number.Value}
                        }
                };

            return Client.Instance().Create(
                _number,
                new IRelationshipAllowingParticipantNode<Number>[0],
                indexEntries);
        }

        private void CreateRelationship()
        {
            Client.Instance().CreateRelationship(
                _number.Build.Reference,
                new BuildProduced(_number.Reference, "number"));
        }

        public Number Build()
        {
            _number.Reference = CreateNumber();
            CreateRelationship();
            return _number;
        }
    }
}