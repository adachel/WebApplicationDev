using WebApplicationDevSem.DTO;

namespace WebApplicationDevSem.Abstraction
{
    public interface IStorageRepo
    {
        public void AddStorage(StorageViewModel storageViewModel);
        public IEnumerable<StorageViewModel> GetStorages();
        public void DeleteStorage(int id);
    }
}
