using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DB;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Models;

namespace Sem3GraphQL.Repo
{
    public class StorageRepo : IStorageRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        //private ProductContext _productContext;

        //public StorageRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        //{
        //    _mapper = mapper;
        //    _memoryCache = memoryCache;
        //    _productContext = productContext;
        //}

        public StorageRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }


        public int AddStorage(StorageViewModel storageViewModel)
        {
            using (var productContext = new ProductContext())
            {
                var storageEntity = productContext.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storageViewModel.Name.ToLower()));
                if (storageEntity == null)
                {
                    var entity = _mapper.Map<Storage>(storageViewModel);
                    productContext.Storages.Add(entity);
                    productContext.SaveChanges();
                    _memoryCache.Remove("storages");
                    storageViewModel.Id = entity.Id;
                }
                else
                {
                    throw new Exception("Storage already exist");
                }
                return storageViewModel.Id ?? 0;
            }
        }

        public IEnumerable<StorageViewModel> GetStorages()
        {
            if (_memoryCache.TryGetValue("storages", out List<StorageViewModel>? storageCache))
            {
                return storageCache!;
            }
            using (var productContext = new ProductContext())
            {
                var storages = productContext.Storages.Select(_mapper.Map<StorageViewModel>).ToList();

                _memoryCache.Set("storages", storages, TimeSpan.FromMinutes(30));

                return storages;
            }
        }

        public int DeleteStorage(int id)
        {
            using (var productContext = new ProductContext())
            {
                var storageEntity = productContext.Storages.FirstOrDefault(x => x.Id == id);
                if (storageEntity != null)
                {
                    if (storageEntity.Products.Count() == 0)
                    {
                        productContext.Remove(storageEntity);
                        productContext.SaveChanges();
                        _memoryCache.Remove("storages");
                    }
                    else
                    {
                        throw new Exception("Storage is not empty");
                    }
                }
                else
                {
                    throw new Exception("There is no such storage");
                }
            }
            return id;
        }
    }
}
