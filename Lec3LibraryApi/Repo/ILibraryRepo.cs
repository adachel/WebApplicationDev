using Lec3LibraryApi.DTO;

namespace Lec3LibraryApi.Repo
{
    public interface ILibraryRepo
    {
        public void AddAuthor(AuthorDTO author);
        public void AddBook(BookDTO book);
        public IEnumerable<AuthorDTO> GetAuthors();
        public IEnumerable<BookDTO> GetBooks();
        public bool CheckBook(Guid bookId);
    }
}
