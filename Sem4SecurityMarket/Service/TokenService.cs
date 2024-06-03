using Microsoft.IdentityModel.Tokens;
using Sem4SecurityMarket.Abstraction;
using Sem4SecurityMarket.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;

namespace Sem4SecurityMarket.Service
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
