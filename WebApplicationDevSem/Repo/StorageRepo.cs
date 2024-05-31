using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Repo
{
    public class StorageRepo : IStorageRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private ProductContext _productContext;

        public StorageRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productContext = productContext;
        }

        public void AddStorage(StorageViewModel storageViewModel)
        {
            using (_productContext)
            {
                var storageEntity = _productContext.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storageViewModel.Name.ToLower()));
                if (storageEntity == null)
                {
                    var entity = _mapper.Map<Storage>(storageViewModel);
                    _productContext.Storages.Add(entity);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("storages");
                }
                else
                {
                    throw new Exception("Storage already exist");
                }
            }
        }

        public IEnumerable<StorageViewModel> GetStorages()
        {
            if (_memoryCache.TryGetValue("storages", out List<StorageViewModel>? storageCache))
            {
                return storageCache!;
            }
            using (_productContext)
            {
                var storages = _productContext.Storages.Select(_mapper.Map<StorageViewModel>).ToList();

                _memoryCache.Set("storages", storages, TimeSpan.FromMinutes(30));

                return storages;
            }
        }

        public void DeleteStorage(int id)
        {
            using (_productContext)
            {
                var storageEntity = _productContext.Storages.FirstOrDefault(x => x.Id == id);
                if (storageEntity != null)
                {
                    if (_productContext.ProductsStorage.Count() == 0)
                    {
                        _productContext.Remove(storageEntity);
                        _productContext.SaveChanges();
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
        }


    }
}
