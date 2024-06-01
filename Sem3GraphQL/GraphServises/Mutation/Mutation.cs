using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Repo;

namespace Sem3GraphQL.GraphServises.Mutation
{
    public class Mutation
    {
        private IProductRepo _poductRepo;
        private IProductGroupRepo _productGroupRepo;
        private IStorageRepo _storageRepo;
        private IProductStorageRepo _productStorageRepo;

        public Mutation(IProductRepo poductRepo, IProductGroupRepo productGroupRepo, IStorageRepo storageRepo, IProductStorageRepo productStorageRepo)
        {
            _poductRepo = poductRepo;
            _productGroupRepo = productGroupRepo;
            _storageRepo = storageRepo;
            _productStorageRepo = productStorageRepo;
        }

        public int AddProduct(ProductViewModel productViewModel) => _poductRepo.AddProduct(productViewModel);
        public int UpdateProduct(int id, float price) => _poductRepo.UpdateProduct(id, price);
        public int DeleteProduct(int id) => _poductRepo.DeleteProduct(id);


        public int AddProductGroup(ProductGroupViewModel productGroupViewModel) => _productGroupRepo.AddProductGroup(productGroupViewModel);
        public int DeleteProductGroup(int id) => _productGroupRepo.DeleteProductGroup(id);

        public int AddStorage(StorageViewModel storageViewModel) => _storageRepo.AddStorage(storageViewModel);
        public int DeleteStorage(int id) => _storageRepo.DeleteStorage(id);

        public int AddProductToStorage(ProductStorageViewModel productSrorageViewModel) => _productStorageRepo.AddProductToStorage(productSrorageViewModel);
        public int UpdateProductInStorage(int storageId, int productId, int count) => _productStorageRepo.UpdateProductInStorage(storageId, productId, count);
        public int DeleteProductInStorage(int productId) => _productStorageRepo.DeleteProductInStorage(productId);
    } 
}
