using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string email, string role, string FirstName, string LastName, int Id)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, role),
        new Claim(ClaimTypes.GivenName, FirstName),
        new Claim(ClaimTypes.Surname, LastName),
        new Claim(ClaimTypes.NameIdentifier, Id.ToString())
    };

            var keyString = _config["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = _config["JwtSettings:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = _config["JwtSettings:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured.");
            var expiresStr = _config["JwtSettings:Expires"] ?? throw new InvalidOperationException("JWT Expires is not configured.");
            var expires = double.Parse(expiresStr);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
