using HW4.Abstraction;
using HW4.DTO;
using HW4.Repo;
using HW4.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IUserRepo _userRepo;
        private RoleIdToUserRole _roleIdToUserRole;

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] UserDTO user)
        {
            try
            {
                _userRepo.UserAdd(user.Name, user.Password, (Models.RoleId)user.UserRole);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserDTO userLogin)
        {
            try
            {
                var roleId = _userRepo.UserCheck(userLogin.Name, userLogin.Password);
                var user = new UserDTO { Name = userLogin.Name, UserRole = _roleIdToUserRole.UserRoleToRoleId(roleId) };
                var token = GenerateToken(user);

                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
