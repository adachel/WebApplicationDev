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
                else
                {
                    throw new Exception("ProductGroup already exist");
                }
            }
        }


        public IEnumerable<ProductGroupViewModel> GetProdutGroups()
        {
            if (_memoryCache.TryGetValue("groups", out List<ProductGroupViewModel>? productGroupsCache)) 
            {
                return productGroupsCache!;
            }
            using (var context = new ProductContext())
            {
                var groups = context.ProductGroup.Select(_mapper.Map<ProductGroupViewModel>).ToList();
                _memoryCache.Set("groups", groups, TimeSpan.FromMinutes(30));   
                return groups;
            }
        }


        public void DeleteProductGroup(int id)
        {
            using (var context = new ProductContext())
            {
                if (context.ProductGroup.Count(x => x.Id == id) > 0)
                {
                    var entityProductGroup = context.ProductGroup.FirstOrDefault(x => x.Id == id);
                    if (context.Products.FirstOrDefault(x => x.ProductGroupId == entityProductGroup!.Id) == null)
                    {
                        context.ProductGroup.Remove(entityProductGroup!);
                        context.SaveChanges();
                        _memoryCache.Remove("groups");
                    }
                    else
                    {
                        throw new Exception("Group is not empty");
                    }
                }
                else
                {
                    throw new Exception("Group does not exist");
                }
            }
        }


    }
}
