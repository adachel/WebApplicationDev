using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IProductStorageRepo
    {
        public void AddProductToStorage(ProductStorageViewModel productSrorageViewModel);
        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage();
        public void UpdateProductInStorage(int productId, int count);
        public void DeleteProductInStorage(int productId);
    }
}
