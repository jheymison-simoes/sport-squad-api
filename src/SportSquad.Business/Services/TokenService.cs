using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Resources;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Services;

public class TokenService : BaseService, ITokenService
{
    
    public TokenService(
        IMapper mapper, 
        AppSettings appSettings, 
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
    }
    
    public (string token, DateTime expireDate) GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AppSettings.SecretToken);
        var expireDate = DateTime.UtcNow.AddHours(8);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString())
            }),
            Expires = expireDate,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), expireDate);
    }
}