using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupRepo _groupRepo;

        public ProductGroupController(IProductGroupRepo productGroupRepo)
        {
            _groupRepo = productGroupRepo;
        }


        [HttpPost(template: "AddGroup")]
        public ActionResult AddGroup(ProductGroupViewModel productGroupViewModel)
        {
            try
            {
                _groupRepo.AddProductGroup(productGroupViewModel);
                return Ok();
            }
            catch
            {
                return StatusCode(409);
            }
        }


        [HttpGet(template: "GetGroups")]
        public ActionResult<IEnumerable<ProductGroupViewModel>> GetGroups()
        {
            try
            {
                return Ok(_groupRepo.GetProdutGroups());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }


        }


        [HttpDelete(template: "DeleteGroup")]
        public ActionResult DeleteGroup(int id)
        {
            try
            {
                _groupRepo.DeleteProductGroup(id);
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }
        }
    }
}
