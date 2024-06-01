using Lec4JWTAuth.AuthorizatoinModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lec4JWTAuth.Controllers
{
    //public static class RsaTools // когда используется RSA
    //{
    //    public static RSA GetPrivatKey()
    //    {
    //        var f = File.ReadAllText("rsa/privat_key.pem");
    //        var rsa = RSA.Create();
    //        rsa.ImportFromPem(f);
    //        return rsa;
    //    }
    //}



    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserAuthenticationService _authenticationService;

        public LoginController(IConfiguration config, IUserAuthenticationService authenticationService)
        {
            _config = config;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]    
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel userLogin)
        {
            var user = _authenticationService.Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateTokin(user);

                return Ok(token);
            }
            return NotFound("UserNotFound");
        }
        private string GenerateTokin(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"])); // получаем ключ
            //var key = new RsaSecurityKey(RsaTools.GetPrivatKey()); // для RSA
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature); // для RSA

            var claims = new[] // созд объект
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], // создаем токен
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
