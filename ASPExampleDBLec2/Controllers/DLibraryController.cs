using ASPExampleDBLec2.DB;
using ASPExampleDBLec2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace ASPExampleDBLec2.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DLibraryController : ControllerBase
    {
        private AppDbContext _context;
        private IDistributedCache _memoryCache;

        public DLibraryController(AppDbContext context, IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        private T GetData<T>(string key)
        {
            var value = _memoryCache.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        private void SetData<T>(string key, T value)
        {
            string jsonString = JsonSerializer.Serialize(value);
            _memoryCache.SetString(key, jsonString);
        }

        private bool TryGetValue<T>(string key, out T value)
        {
            var data = GetData<T>(key);
            if (data == null)
            {
                value = default;
                return false;
            }
            else
            {
                value = data;
                return true;
            }
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
                    _memoryCache.Remove("authors");
                    return Ok();
                }
            }
        }

        [HttpGet(template: "GetAuthor")]
        public ActionResult<IEnumerable<AuthorModel>> GetAuthor()
        {
            if (TryGetValue("authors", out List<AuthorModel> authors))
            {
                return Ok(authors);
            }

            using (_context)
            {
                authors = _context.Authors.Select(x => new AuthorModel { Name = x.Name, Id = x.ID }).ToList();

                SetData("authors", authors); // прежде, чем получить отправляем в кэш

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
                        _memoryCache.Remove("books"); // удаление из кэша
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
            if (TryGetValue("books", out List<BookModel> books))
            {
                return Ok(books);
            }

            using (_context)
            {
                books = _context.Books.Select(x => new BookModel { Author = x.Author.Name, Title = x.Title }).ToList();
                SetData("books", books);
                return Ok(books);
            }
        }

        [HttpPost(template: "AddBookWithAuthorName")]
        public ActionResult AddBookWithAuthorName(BookModel bm) // принимаем модель
        {
            try
            {
                using (_context)
                {
                    var title = bm.Title;
                    var authorId = _context.Authors.FirstOrDefault(x => x.Name == bm.Author)?.ID ?? -1;
                    // ищем автора по названию автора из модели

                    if (authorId < 0) // если не находим, то добавляем
                    {
                        var author = new Author { Name = bm.Author };
                        _context.Authors.Add(author);
                        _context.SaveChanges();
                        authorId = author.ID;
                    }

                    if (_context.Books.FirstOrDefault(x => x.Title == title && x.AuthorId == authorId) != null)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        _context.Books.Add(new Book { Title = title, AuthorId = authorId });
                        _context.SaveChanges();
                        _memoryCache.Remove("books");
                        return Ok();
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        private string GetCsv(IEnumerable<BookModel> books)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in books)
            {
                sb.AppendLine(b.Author + ";" + b.Title + "\n");
            }
            return sb.ToString();
        }

        [HttpGet(template: "GetBooksCsv")]
        public FileContentResult GetBookCsv()
        {
            var content = "";
            if (TryGetValue("books", out List<BookModel> books))
            {
                content = GetCsv(books);
            }
            else
            {
                using (_context)
                {
                    books = _context.Books.Select(x => new BookModel { Author = x.Author.Name, Title = x.Title }).ToList();
                    content = GetCsv(books);
                }
            }
            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }

        //// для работы со статичными файлами
        [HttpGet(template: "GetBooksCsvUrl")]
        public ActionResult<string> GetBookCsvUrl()
        {
            var content = "";
            using (_context) // оплучаем csv файл
            {
                var books = _context.Books.Select(x => new BookModel { Author = x.Author.Name, Title = x.Title }).ToList();
                SetData("books", books.Select(x => new BookModel { Author = x.Author.ToUpper(), Title = x.Title }).ToList());

                content = GetCsv(books);
            }

            string fileName = null;

            fileName = "books" + DateTime.Now.ToBinary().ToString() + ".csv"; // называем, полученный файл по шаблону

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StatisticFiles", fileName), content); // созраняем в каталог StaticFiles

            return "http://" + Request.Host.ToString() + "/static/" + fileName; // возвращаем ссылку на файл
        }


    }
}
