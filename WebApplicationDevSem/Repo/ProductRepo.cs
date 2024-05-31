using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Repo
{
    public class ProductRepo : IPoductRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private ProductContext _productContext;

        public ProductRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productContext = productContext;
        }

        public void AddProduct(ProductViewModel productViewModel)
        {
            using (_productContext)
            {
                var entityProduct = _productContext.Products.FirstOrDefault(x => x.Name!.ToLower().Equals(productViewModel.Name!.ToLower()));
                if (entityProduct == null)
                {
                    var entity = _mapper.Map<Product>(productViewModel);
                    _productContext.Products.Add(entity);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                else
                {
                    throw new Exception("Product already exist");
                }
            }
        }


        public IEnumerable<ProductViewModel> GetProduts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductViewModel>? productsCache)) // проверяет, если кэш не пустой
            {
                return productsCache!;
            }
            using (_productContext)
            {
                var products = _productContext.Products.Select(_mapper.Map<ProductViewModel>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));   //"products" - ключ, products - что кэшируем,
                                                                                    //TimeSpan.FromMinutes(30) - время кэша
                return products;
            }
        }


        public void UpdateProduct(int id, float price)
        {
            using (_productContext)
            {
                if (_productContext.Products.Count(x => x.Id == id) > 0)
                {
                    var x = _productContext.Products.FirstOrDefault(x => x.Id == id);
                    x!.Price = price;
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
        }


        public void DeleteProduct(int id)
        {
            using (_productContext)
            {
                if (_productContext.Products.Count(x => x.Id == id) > 0)
                {
                    var x = _productContext.Products.FirstOrDefault(x => x.Id == id);
                    _productContext.Products.Remove(x!);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
        }
    }
}
