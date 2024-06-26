﻿using AutoMapper;
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
        private ProductContext _productContext;

        public ProductGroupRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productContext = productContext;
        }

        public ProductGroupRepo()
        {
        }

        public void AddProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            using (_productContext)
            {
                var entityGroup = _productContext.ProductsGroup.FirstOrDefault(x => x.Name!.ToLower().Equals(productGroupViewModel.Name!.ToLower()));
                if (entityGroup == null)
                {
                    var entity = _mapper.Map<ProductGroup>(productGroupViewModel);
                    _productContext.ProductsGroup.Add(entity);
                    _productContext.SaveChanges();
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
            using (_productContext)
            {
                var groups = _productContext.ProductsGroup.Select(_mapper.Map<ProductGroupViewModel>).ToList();
                _memoryCache.Set("groups", groups, TimeSpan.FromMinutes(30));   
                return groups;
            }
        }


        public void DeleteProductGroup(int id)
        {
            using (_productContext)
            {
                if (_productContext.ProductsGroup.Count(x => x.Id == id) > 0)
                {
                    var entityProductGroup = _productContext.ProductsGroup.FirstOrDefault(x => x.Id == id);
                    if (_productContext.Products.FirstOrDefault(x => x.ProductGroupId == entityProductGroup!.Id) == null)
                    {
                        _productContext.ProductsGroup.Remove(entityProductGroup!);
                        _productContext.SaveChanges();
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
