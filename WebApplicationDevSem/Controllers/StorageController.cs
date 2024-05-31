using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepo _storageRepo;

        public StorageController(IStorageRepo storageRepo)
        {
            _storageRepo = storageRepo;
        }

        [HttpPost(template: "AddStorage")]
        public ActionResult AddStorage(StorageViewModel storageViewModel)
        {
            try
            {
                _storageRepo.AddStorage(storageViewModel);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(409);
            }
        }

        [HttpGet(template: "GetStorages")]
        public ActionResult<IEnumerable<StorageViewModel>> GetStorage() 
        {
            try
            {
                return Ok(_storageRepo.GetStorages());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "DeleteStorage")]
        public ActionResult DeleteStorage(int id) 
        {
            try
            {
                _storageRepo.DeleteStorage(id);
                return Ok();    
            }
            catch 
            {
                return StatusCode(409);
            }
        }


    }
}
