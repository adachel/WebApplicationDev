using Lec3ApiClientsBook.Client;
using Lec3ApiClientsBook.DTO;
using Lec3ApiClientsBook.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Lec3ApiClientsBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientBooksController : ControllerBase
    {
        private IClientBookRepo _repo;

        public ClientBooksController(IClientBookRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "TakeBook")]
        public async Task<TakeBookResultDTO> TakeBookAsync(ClientBookDTO record)
        {
            var userExistsTask = new LibraryUsersClient().Exists(record.ClientId);
            var bookExistsTask = new LibraryBooksClient().Exists(record.BookId);

            var userExists = await userExistsTask;
            var bookExists = await bookExistsTask;

            if (userExists && bookExists)
            {
                try
                {
                    _repo.TakeBook(record);
                    return new TakeBookResultDTO { Success = true };
                }
                catch (Exception e)
                {
                    if (e is DbUpdateException && e.InnerException is PostgresException && e?.InnerException?.Message?.Contains("duplicate") == true)
                    {
                        return new TakeBookResultDTO { Error = "Такую книгу уже взяли" };
                    }
                    throw;
                }
            }
            else
            {
                if (!userExists)
                {
                    return new TakeBookResultDTO { Error = "Такой пользователь не найден" };
                }
                else 
                {
                    return new TakeBookResultDTO { Error = "Книга не найдена" };
                }
            }
        }

        [HttpPost(template: "ReturnBook")]
        public ActionResult ReturnBook(Guid bookId)
        { 
            _repo.ReturnBook(bookId);
            return Ok();
        }

        [HttpGet(template: "ListBooks")]
        public ActionResult<IEnumerable<Guid>> ListBooks(Guid clientId)
        {
            return Ok(_repo.ListBooks(clientId));
        }
    }
}
