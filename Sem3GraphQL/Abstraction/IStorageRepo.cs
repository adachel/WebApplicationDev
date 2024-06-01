using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IStorageRepo
    {
        public void AddStorage(StorageViewModel storageViewModel);
        public IEnumerable<StorageViewModel> GetStorages();
        public void DeleteStorage(int id);
    }
}
