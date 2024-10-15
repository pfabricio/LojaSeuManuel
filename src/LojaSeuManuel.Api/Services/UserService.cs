using System.IdentityModel.Tokens.Jwt;
using LojaSeuManuel.Api.Models;

using System.Security.Claims;
using System.Text;
using LojaSeuManuel.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LojaSeuManuel.Api.Services;

public class UserService: IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> AuthenticateAsync(LoginModel login)
    {
        if (login.Usuario == "root" && login.Senha == "123456")
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var jwtExpiryMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Usuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtExpiryMinutes),
                signingCredentials: creds);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return Task.FromResult<string>(null);
    }
}