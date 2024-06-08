using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DB;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Models;

namespace Sem3GraphQL.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private ProductContext _productContext;

        public ProductRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public ProductRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext) : this(mapper, memoryCache)
        {
            _productContext = productContext;
        }

        public int AddProduct(ProductViewModel productViewModel)
        {
            using (var productContext = new ProductContext())
            {
                var entityProduct = _productContext.Products.FirstOrDefault(x => x.Name!.ToLower().Equals(productViewModel.Name!.ToLower()));
                if (entityProduct == null)
                {
                    var entity = _mapper.Map<Product>(productViewModel);
                    _productContext.Products.Add(entity);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                    productViewModel.Id = entity.Id;
                }
                else
                {
                    throw new Exception("Product already exist");
                }
            }
            return productViewModel.Id ?? 0; // означает, если Id = 0, то вернем 0
        }

        public IEnumerable<ProductViewModel> GetProduts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductViewModel>? productsCache)) // проверяет, если кэш не пустой
            {
                return productsCache!;
            }
            using (var productContext = new ProductContext())
            {
                productContext.connectionString = _productContext.connectionString;
                var products = productContext.Products.Select(_mapper.Map<ProductViewModel>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));   //"products" - ключ, products - что кэшируем,
                                                                                    //TimeSpan.FromMinutes(30) - время кэша
                return products;
            }
        }

        public int UpdateProduct(int id, float price)
        {
            using (var productContext = new ProductContext())
            {
                if (_productContext.Products.Count(x => x.Id == id) > 0)
                {
                    var x = _productContext.Products.FirstOrDefault(x => x.Id == id);
                    x.Price = price;
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
            return id;
        }

        public int DeleteProduct(int id)
        {
            using (var productContext = new ProductContext())
            {
                if (_productContext.Products.Count(x => x.Id == id) > 0)
                {
                    var x = _productContext.Products.FirstOrDefault(x => x.Id == id);
                    _productContext.Products.Remove(x);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("products");
                }
                else
                {
                    throw new Exception("There is no such product");
                }
            }
            return id;
        }
    }
}
