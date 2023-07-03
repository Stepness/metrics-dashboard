using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MetricsMonitoringServer.Settings;
using Microsoft.IdentityModel.Tokens;

namespace MetricsMonitoringServer.Services;

public static class JwtTokenHelper
{
    public static string GenerateJsonWebToken(User model)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecureKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "Me",
            "You",
            new[]
            {
                new Claim("Role", model.Role),
                new Claim(JwtRegisteredClaimNames.Sub, model.Username)
            },
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}