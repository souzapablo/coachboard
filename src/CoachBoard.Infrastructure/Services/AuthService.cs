using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoachBoard.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoachBoard.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new("email", email),
            new(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string ComputeSha256Hash(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        var builder = new StringBuilder();

        foreach (var t in bytes)
            builder.Append(t.ToString("x2"));

        return builder.ToString();
    }
}