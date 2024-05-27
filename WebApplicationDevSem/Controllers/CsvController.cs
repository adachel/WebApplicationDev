using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : ControllerBase
    {
        private ProductContext _context;
        private IDistributedCache _memoryCache;

        public CsvController(ProductContext productContext, IDistributedCache memoryCache)
        {
            _context = productContext;
            _memoryCache = memoryCache;
        }



        private T GetData<T>(string key)
        {
            var value = _memoryCache.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value)!;
            }
            return default!;
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
                value = default!;
                return false;
            }
            else
            {
                value = data;
                return true;
            }
        }
        private string GetCsv(IEnumerable<ProductViewModel> product)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (var p in product)
            {
                sb.AppendLine(p.ProductGroupId + ";" + p.Name + ";" + p.Price + "\n");
            }
            return sb.ToString();
        }

        [HttpGet(template: "GetProductCsv")]
        public FileContentResult GetProductCsv()
        {
            var content = "";
            if (TryGetValue("producs", out List<ProductViewModel> products))
            {
                content = GetCsv(products);
            }
            else
            {
                using (_context)
                {
                    products = _context.Products.Select(x => new ProductViewModel { ProductGroupId = x.ProductGroupId, Name = x.Name }).ToList();

                    content = GetCsv(products);
                }
            }
            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }
    }
}
