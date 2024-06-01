using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IProductStorageRepo
    {
        public int AddProductToStorage(ProductStorageViewModel productSrorageViewModel);
        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage();
        public int UpdateProductInStorage(int storageId, int productId, int count);
        public int DeleteProductInStorage(int productId);
    }
}
