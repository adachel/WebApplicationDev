namespace Lec3ApiClientsBook.Client
{
    public interface ILibraryBooksClient
    {
        public Task<bool> Exists(Guid? id);
    }
}
