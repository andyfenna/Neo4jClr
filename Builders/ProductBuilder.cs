using System.Linq;
using Neo4jClient;
using Neo4jClr.Nodes;
using Neo4jClr.Relationships;

namespace Neo4jClr.Builders
{
    public class ProductBuilder
    {
        private Product _product;

        public ProductBuilder WithName(string productName)
        {
            if (_product == null)
            {
                _product = new Product();
            }

            _product.Name = productName;

            return this;
        }

        private NodeReference<Product> GetProduct()
        {
            var productNode = Client.Instance().RootNode.StartCypher("root")
                .Match("root-[:ROOT_HAS_PRODUCT]->product")
                .Where("product.ProductName  = '" + _product.Name + "'")
                .Return<Node<Product>>("product")
                .Results
                .FirstOrDefault();

            return productNode != null ? productNode.Reference : null;
        }

        private NodeReference<Product> CreateProduct()
        {
            var indexEntries = new[]
                {
                    new IndexEntry("product")
                        {
                            {"name", _product.Name}
                        }
                };

            return Client.Instance().Create(
                _product,
                new IRelationshipAllowingParticipantNode<Product>[0],
                indexEntries);
        }

        public Product Build()
        {
            _product.Reference = GetProduct();

            if (_product.Reference == null)
            {
                _product.Reference = CreateProduct();
                CreateRelationship();
            }
            
            return _product;
        }

        private void CreateRelationship()
        {

            Client.Instance().CreateRelationship(
                Client.Instance().RootNode,
                new RootHasProduct(_product.Reference));
        }
    }
}