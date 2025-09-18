using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EV_Station.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;

        public TokenService(IConfiguration configuration, IUnitOfWork uow)
        {
            _configuration = configuration;
            _uow = uow;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = GetClaims(user);
            var creds = GetCredentials();
            var expires = GetExpries();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Audience = _configuration["Authentication:Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = creds
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
        }

        public string GenerateAndMapRefreshToken(User user)
        {
            var userRepository = _uow.Users;

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            userRepository.Update(user);
            return refreshToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                };
            return claims;
        }

        private SigningCredentials GetCredentials()
        {
            var secretKey = _configuration["Authentication:Jwt:Key"];
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 16)
                throw new ArgumentException("JWT secret key must be at least 16 characters.");
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        }
        private DateTime GetExpries() => DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:Jwt:ExpiresMinutes"] ?? "30"));
    }
}
