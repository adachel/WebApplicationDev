using Lec3UserApi.DTO;
using Lec3UserApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Lec3UserApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost(template: "AddUser")]
        public ActionResult AddUser(UserDTO user)
        { 
            _userRepository.AddUser(user);
            return Ok();
        }

        [HttpGet(template: "Exists")]
        public ActionResult<bool> Exists(string email)
        {
            return Ok(_userRepository.Exist(email));
        }

        [HttpGet(template: "ExistsId")]
        public ActionResult<bool> ExistsId(Guid id)
        {
            return Ok(_userRepository.Exist(id));
        }
    }
}
