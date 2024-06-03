using Lec4JWTAuth.AuthorizatoinModel;
using Lec4JWTAuth.DB;
using Lec4JWTAuth.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lec4JWTAuth.Controllers
{
    public static class RSATools // когда используется RSA
    {
        public static RSA GetPrivatKey()
        {
            var f = File.ReadAllText("RSA/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
    }



    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserAuthenticationService _authenticationService;

        private readonly IUserRepository _userRepository;

        public LoginController(IConfiguration config, IUserAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _config = config;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        private static UserRole RoleIdToUserRole(RoleId id)
        {
            if (id == RoleId.Admin)
            { 
                return UserRole.Administrator;
            }
            else return UserRole.User;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] LoginModel userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.Name, userLogin.Password, DB.RoleId.Admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] LoginModel userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.Name, userLogin.Password, DB.RoleId.User);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }


        [AllowAnonymous]    
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel userLogin)
        {
            try
            {
                var roleId = _userRepository.UserCheck(userLogin.Name, userLogin.Password);
                var user = new UserModel { UserName = userLogin.Name, Role = RoleIdToUserRole(roleId) };
                var token = GenerateToken(user);

                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Login([FromBody] LoginModel userLogin)
        //{
        //    var user = _authenticationService.Authenticate(userLogin);
        //    if (user != null)
        //    {
        //        var token = GenerateTokin(user);

        //        return Ok(token);
        //    }
        //    return NotFound("UserNotFound");
        //}






        private string GenerateToken(UserModel user)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"])); // получаем ключ
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var key = new RsaSecurityKey(RSATools.GetPrivatKey()); // для RSA
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature); // для RSA

            var claims = new[] // созд объект
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], // создаем токен
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15), // время токена
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
