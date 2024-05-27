using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Repo;

namespace Sem3GraphQL
{
    public class Query
    {
        //private IPoductRepo _product;

        //public Query(IPoductRepo product)
        //{
        //    _product = product;
        //}

        public IEnumerable<ProductViewModel> GetProducts([Service] ProductRepo product) => product.GetProduts();
    }
}
