using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IPoductRepo
    {
        public void AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProduts();
        public void UpdateProduct(int id, float price);
        public void DeleteProduct(int id);
    }
}
