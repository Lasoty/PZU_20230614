using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PzuCwiczenia.Services.Authentication;

public class AuhtenticationService : IAuthenticationService
{
    private readonly IConfiguration configuration;
    private readonly IUserService userService;

    public AuhtenticationService(IConfiguration configuration, IUserService userService)
    {
        this.configuration = configuration;
        this.userService = userService;
    }

    public string Auhtenticate(string username, string password)
    {
        if (!userService.ValidateCredentials(username, password))
            throw new InvalidOperationException("Uwierzytelnianie nie powiodło się.");

        var token = GenerateJwtToken(username);
        return token;
    }

    private string GenerateJwtToken(string username)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
            }, JwtBearerDefaults.AuthenticationScheme),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
