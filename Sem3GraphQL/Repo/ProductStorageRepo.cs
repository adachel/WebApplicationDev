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
        private ProductContext _context;
        private IMapper _mapper;
        private IMemoryCache _cache;

        public ProductStorageRepo(ProductContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public void AddProductToStorage(ProductStorageViewModel productSrorageViewModel)
        {
            using (_context)
            {
                var storageEntity = _context.Storages.FirstOrDefault(x => x.Id == productSrorageViewModel.StorageId);
                var productEntity = _context.Products.FirstOrDefault(x => x.Id == productSrorageViewModel.ProductId);
                var productStorageEntity = _context.ProductsStorage.FirstOrDefault(x => x.ProductId == productEntity.Id);

                if (storageEntity != null)
                {
                    if (productEntity != null)
                    {
                        if (productStorageEntity == null)
                        {
                            var entity = _mapper.Map<ProductStorage>(productSrorageViewModel);
                            _context.ProductsStorage.Add(entity);
                            _context.SaveChanges();
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
            }
        }

        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage()
        {
            if (_cache.TryGetValue("produtsStorage", out List<ProductStorageViewModel> productStorageCache))
            {
                return productStorageCache;
            }
            using (_context)
            {
                var productsStorage = _context.ProductsStorage.Select(_mapper.Map<ProductStorageViewModel>).ToList();
                _cache.Set("produtsStorage", productsStorage, TimeSpan.FromMinutes(30));
                return productsStorage;
            }
        }

        public void UpdateProductInStorage(int productId, int count)
        {
            using (_context)
            {
                if (_context.ProductsStorage.Count(x => x.ProductId == productId) > 0)
                {
                    var productStorageEntity = _context.ProductsStorage.FirstOrDefault(p => p.ProductId == productId);
                    productStorageEntity.Count = count;
                    _context.SaveChanges();
                    _cache.Remove("produtsStorage");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
        }

        public void DeleteProductInStorage(int productId)
        {
            using (_context)
            {
                if (_context.ProductsStorage.Count(x => x.ProductId == productId) > 0)
                {
                    var productStorageEntity = _context.ProductsStorage.FirstOrDefault(p => p.ProductId == productId);
                    _context.Remove(productStorageEntity);
                    _context.SaveChanges();
                    _cache.Remove("produtsStorage");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
        }
    }
}
