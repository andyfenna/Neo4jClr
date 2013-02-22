using Neo4jClient;
using Neo4jClr.Nodes;

namespace Neo4jClr.Relationships
{
    public class ResultItemMovedTo :
        Relationship,
        IRelationshipAllowingSourceNode<ResultItem>
    {
        private readonly string _keyType;

        public ResultItemMovedTo(ResultItem targetNode, string key) : base(targetNode.Reference)
        {
            _keyType = string.Format("{0}_MOVED_TO_{1}",  key.ToUpper(), targetNode.Type.ToUpper());
            Direction = RelationshipDirection.Automatic;
        }

        public override string RelationshipTypeKey
        {
            get { return _keyType; }
        }
    }
}