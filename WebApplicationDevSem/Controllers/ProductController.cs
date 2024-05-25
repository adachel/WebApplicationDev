using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IPoductRepo _productRepo;

        public ProductController(IPoductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpPost(template: "AddProduct")]
        public ActionResult AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                _productRepo.AddProduct(productViewModel);
                return Ok();
            }
            catch
            {
                return StatusCode(409);
            }
        }


        [HttpGet(template: "GetProducts")]
        public ActionResult<IEnumerable<ProductViewModel>> GetProducts()
        {
            return Ok(_productRepo.GetProduts());
        }


        [HttpPatch(template: "PatchProduct")]
        public ActionResult PatchProduct(int id, float price)
        {
            try
            {
                _productRepo.UpdateProduct(id, price);
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }

        }

        [HttpDelete(template: "DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productRepo?.DeleteProduct(id);
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }

        }
    }
}
