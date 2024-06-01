using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DB;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Models;

namespace Sem3GraphQL.Repo
{
    public class ProductStorageRepo : IProductStorageRepo
    {
        private IMapper _mapper;
        private IMemoryCache _cache;

        public ProductStorageRepo(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProductToStorage(ProductStorageViewModel productSrorageViewModel)
        {
            using (var productContext = new ProductContext())
            {
                var storageEntity = productContext.Storages.FirstOrDefault(x => x.Id == productSrorageViewModel.StorageId);
                var productEntity = productContext.Products.FirstOrDefault(x => x.Id == productSrorageViewModel.ProductId);
                var productStorageEntity = productContext.ProductsStorage.FirstOrDefault(x => x.ProductId == productEntity.Id);

                if (storageEntity != null)
                {
                    if (productEntity != null)
                    {
                        if (productStorageEntity == null)
                        {
                            var entity = _mapper.Map<ProductStorage>(productSrorageViewModel);
                            productContext.ProductsStorage.Add(entity);
                            productContext.SaveChanges();
                            _cache.Remove("produtsStorage");
                        }
                        else
                        {
                            throw new Exception("Product already exist");
                        }
                    }
                    else
                    {
                        throw new Exception("Product is not in the database");
                    }
                }
                else
                {
                    throw new Exception("Storage is not in the database");
                }
                return productSrorageViewModel.ProductId;
            }

        }

        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage()
        {
            if (_cache.TryGetValue("produtsStorage", out List<ProductStorageViewModel> productStorageCache))
            {
                return productStorageCache;
            }
            using (var productContext = new ProductContext())
            {
                var productsStorage = productContext.ProductsStorage.Select(_mapper.Map<ProductStorageViewModel>).ToList();
                _cache.Set("produtsStorage", productsStorage, TimeSpan.FromMinutes(30));
                return productsStorage;
            }
        }

        public int UpdateProductInStorage(int storageId, int productId, int count)
        {
            using (var productContext = new ProductContext())
            {
                if (productContext.ProductsStorage.FirstOrDefault(x => x.StorageId == storageId) != null)
                {
                    var productInStorage = productContext.ProductsStorage.FirstOrDefault(x => x.StorageId == storageId && x.ProductId == productId);

                    if (productInStorage != null)
                    {
                        var productStorageEntity = productContext.ProductsStorage.FirstOrDefault(p => p.ProductId == productId);
                        productStorageEntity.Count = count;
                        productContext.SaveChanges();
                        _cache.Remove("produtsStorage");
                    }
                    else
                    {
                        throw new Exception("There is no such product");
                    }
                }
                else 
                {
                    throw new Exception("There is no such storage");
                }
            }
            return productId;
        }

        public int DeleteProductInStorage(int productId)
        {
            using (var productContext = new ProductContext())
            {
                if (productContext.ProductsStorage.Count(x => x.ProductId == productId) > 0)
                {
                    var productStorageEntity = productContext.ProductsStorage.FirstOrDefault(p => p.ProductId == productId);
                    productContext.Remove(productStorageEntity);
                    productContext.SaveChanges();
                    _cache.Remove("produtsStorage");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
            return productId;
        }
    }
}
