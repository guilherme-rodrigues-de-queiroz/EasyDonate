using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Auth;
using EasyDonate.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyDonate.Persistence.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _options;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public TokenResultDTO GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("Active", user.Active.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Key)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new TokenResultDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = _options.ExpirationMinutes * 60
            };
        }
    }
}
