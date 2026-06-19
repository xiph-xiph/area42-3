using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend_Area42_3.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Backend_Area42_3.Services;

public class JWTTokenGenerator : ITokenGenerator
{
    public string GenerateToken(int userId, UserRole role)
    {
        string key_string =
            Environment.GetEnvironmentVariable("JwtSecretKey")
            ?? throw new InvalidOperationException("JwtSecretKey environment variable is not set.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key_string));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString()),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
