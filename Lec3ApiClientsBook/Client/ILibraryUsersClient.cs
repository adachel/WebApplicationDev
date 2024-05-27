namespace Lec3ApiClientsBook.Client
{
    public interface ILibraryUsersClient
    {
        public Task<bool> Exists(Guid? id);
    }
}
