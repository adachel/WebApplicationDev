using ASPExampleDBLec2.DB;
using ASPExampleDBLec2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ASPExampleDBLec2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private AppDbContext _context;
        private IMemoryCache _memoryCache;

        public LibraryController(AppDbContext context, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _context = context;
        }


        [HttpPost(template: "AddAuthor")]
        public ActionResult AddAuthor(string name)
        {
            using (_context)
            {
                if (_context.Authors.FirstOrDefault(x => x.Name == name) != null)
                {
                    return StatusCode(409);
                }
                else
                {
                    _context.Authors.Add(new Author { Name = name });
                    _context.SaveChanges();
                    return Ok();
                }
            }
        }

        [HttpGet(template: "GetAuthor")]
        public ActionResult<IEnumerable<AuthorModel>> GetAuthor()
        {
            if (_memoryCache.TryGetValue("authors", out List<AuthorModel> authors))
            {
                return Ok(authors);
            }

            using (_context)
            {
                authors = _context.Authors.Select(x => new AuthorModel { Name = x.Name, Id = x.ID }).ToList();

                _memoryCache.Set("authors", authors); // прежде, чем получить отправляем в кэш

                return Ok(authors);
            }
        }

        [HttpPost(template: "AddBook")]
        public ActionResult AddBook(string title, int autorId)
        {
            using (_context)
            {
                try // если нет autothorId в базе
                {
                    if (_context.Books.FirstOrDefault(x => x.Title == title && x.AuthorId == autorId) != null)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        _context.Books.Add(new Book { Title = title, AuthorId = autorId });
                        _context.SaveChanges();
                        return Ok();
                    }
                }
                catch
                {
                    return StatusCode(500);
                }

            }
        }

        [HttpGet(template: "GetBook")]
        public ActionResult<IEnumerable<BookModel>> GetBooks()
        {
            using (_context)
            {
                var books = _context.Books.Select(x => new BookModel { Author = x.Author.Name, Title = x.Title }).ToList();
                return Ok(books);   
            }
        }




    }
}
