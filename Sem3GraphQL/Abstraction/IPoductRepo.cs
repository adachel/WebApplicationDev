using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IProductRepo
    {
        public int AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProduts();
        public int UpdateProduct(int id, float price);
        public int DeleteProduct(int id);
    }
}
