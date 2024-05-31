using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductStorageController : ControllerBase
    {
        private readonly IProductStorageRepo _repo;

        public ProductStorageController(IProductStorageRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "AddProductToStorage")]
        public ActionResult AddProductToStorage(ProductStorageViewModel productSrorageViewModel) 
        {
            try
            {
                _repo.AddProductToStorage(productSrorageViewModel);
                return Ok();
            }
            catch 
            {
                return StatusCode(409);
            }

        }

        [HttpGet(template: "GetProductsFromStorage")]
        public ActionResult<IEnumerable<ProductStorageViewModel>> GetProductsFromStorage()
        {
            try
            {
                return Ok(_repo.GetProductsFromStorage());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "UpdateProductInStorage")]
        public ActionResult UpdateProductInStorage(int productId, int count)
        {
            try
            {
                _repo.UpdateProductInStorage(productId, count);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(404);
            }   
        }

        [HttpDelete(template: "DeleteProductInStorage")]
        public ActionResult DeleteProductInStorage(int productId)
        {
            try
            {
                _repo.DeleteProductInStorage(productId);
                return Ok();    
            }
            catch (Exception)
            {
                return StatusCode(404);
            }
        }
    }
}
