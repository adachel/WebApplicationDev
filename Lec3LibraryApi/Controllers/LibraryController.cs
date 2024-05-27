using Lec3LibraryApi.DTO;
using Lec3LibraryApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Lec3LibraryApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private ILibraryRepo _library;

        public LibraryController(ILibraryRepo library)
        {
            _library = library;
        }

        [HttpPost(template: "AddAuthor")]
        public ActionResult AddAuthor(AuthorDTO author) 
        {
            _library.AddAuthor(author);
            return Ok();
        }

        [HttpGet(template: "GetAuthors")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthor()
        {
            return Ok(_library.GetAuthors());
        }

        [HttpPost(template: "AddBook")]
        public ActionResult AddBook(BookDTO book)
        {
            _library.AddBook(book);
            return Ok();
        }

        [HttpGet(template: "GetBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            return Ok(_library.GetBooks());
        }

        [HttpGet(template: "CheckBooks")]
        public ActionResult<bool> CheckBook(Guid bookId)
        {
            return Ok(_library.CheckBook(bookId));
        }
    }
}
