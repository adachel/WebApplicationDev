using HW4.Abstraction;
using HW4.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HW4.Service
{
    public class TokenService(JwtConfiguration jwt) : ITokenService
    {
        public string GenerateToken(string email, string roleName)
        {
            var credentilas = new SigningCredentials(jwt.GetSigningKey(), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleName)
            };
            var token = new JwtSecurityToken
            (
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentilas
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
