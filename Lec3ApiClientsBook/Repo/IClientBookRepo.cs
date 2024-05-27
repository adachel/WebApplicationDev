using Lec3ApiClientsBook.DTO;

namespace Lec3ApiClientsBook.Repo
{
    public interface IClientBookRepo
    {
        public void TakeBook(ClientBookDTO record);
        public void ReturnBook(Guid bookId);
        public IEnumerable<Guid?> ListBooks(Guid clientId);
    }
}
