using AutoMapper;
using Lec3LibraryApi.DB;
using Lec3LibraryApi.DTO;

namespace Lec3LibraryApi.Repo
{
    public class LibraryRepo : ILibraryRepo
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public LibraryRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddAuthor(AuthorDTO author)
        {
            using (_context)
            {
                _context.Authors.Add(_mapper.Map<Author>(author));
                _context.SaveChanges();
            }
        }

        public void AddBook(BookDTO book)
        {
            using (_context)
            {
                _context.Books.Add(_mapper.Map<Book>(book));
                _context.SaveChanges();
            }
        }

        public bool CheckBook(Guid bookId)
        {
            using (_context)
            {
                return _context.Books.Any(x => x.Id == bookId);
            }
        }

        public IEnumerable<AuthorDTO> GetAuthors()
        {
            using (_context)
            {
                return _context.Authors.Select(_mapper.Map<AuthorDTO>).ToList();
            }
        }

        public IEnumerable<BookDTO> GetBooks()
        {
            using (_context)
            {
                return _context.Books.Select(_mapper.Map<BookDTO>).ToList();
            }
        }
    }
}
