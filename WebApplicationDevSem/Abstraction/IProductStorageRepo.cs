using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Abstraction
{
    public interface IProductStorageRepo
    {
        public void AddProductToStorage(ProductStorageViewModel productSrorageViewModel);
        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage();
        public void UpdateProductInStorage(int productId, int count);
        public void DeleteProductInStorage(int productId);
    }
}
