using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IStorageRepo
    {
        public int AddStorage(StorageViewModel storageViewModel);
        public IEnumerable<StorageViewModel> GetStorages();
        public int DeleteStorage(int id);
    }
}
