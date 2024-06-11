using HW4.Abstraction;
using HW4.DTO;
using HW4.Models;
using HW4.Repo;
using HW4.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(IUserRepo userRepo, ITokenService tokenService) : ControllerBase
    {
        private IUserRepo _userRepo = userRepo;
        private ITokenService _tokenService = tokenService;

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] UserDTO user)
        {
            try
            {
                _userRepo.UserAdd(user.Email, user.Password, (RoleId)user.UserRole);
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
                var roleId = _userRepo.UserCheck(userLogin.Email, userLogin.Password);
                var user = new UserDTO { Email = userLogin.Email, UserRole = userLogin.UserRole };
                
                var token = _tokenService.GenerateToken(user.Email, roleId.ToString());

                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
