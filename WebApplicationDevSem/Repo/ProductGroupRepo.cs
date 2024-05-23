using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Repo
{
    public class ProductGroupRepo : IProductGroupRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;

        public ProductGroupRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public ProductGroupRepo()
        {
        }

        public void AddProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            using (var context = new ProductContext()) 
            {
                var entityGroup = context.ProductGroup.FirstOrDefault(x => x.Name!.ToLower().Equals(productGroupViewModel.Name!.ToLower()));
                if (entityGroup == null) 
                {
                    var entity = _mapper.Map<ProductGroup>(productGroupViewModel);
                    context.ProductGroup.Add(entity);
                    context.SaveChanges();
                    _memoryCache.Remove("groups");
                }
            }
        }


        public IEnumerable<ProductGroupViewModel> GetProdutGroups()
        {
            throw new NotImplementedException();
        }


        public void DeleteProductGroup(int id)
        {
            throw new NotImplementedException();
        }


    }
}
