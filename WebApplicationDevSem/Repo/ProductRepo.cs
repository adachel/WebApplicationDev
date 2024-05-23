using AutoMapper;
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


        public ProductRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }


        public void AddProduct(ProductViewModel productViewModel)
        {
            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productViewModel.Name.ToLower()));
                if (entityProduct == null) 
                {
                    var entity = _mapper.Map<Product>(productViewModel);
                    context.Products.Add(entity);
                    context.SaveChanges();
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
            using (var context = new ProductContext())
            {
                var products = context.Products.Select(_mapper.Map<ProductViewModel>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));   //"products" - ключ, products - что кэшируем,
                                                                                    //TimeSpan.FromMinutes(30) - время кэша
                return products;
            }
        }
    }
}
