using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EV_Station.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {

        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public string HashPassword(string clientHashedPassword)
        {
            return passwordHasher.HashPassword(null!, clientHashedPassword);
        }

        public bool VerifyPassword(string clientHashedPassword, string storedHash)
        {
            var result = passwordHasher.VerifyHashedPassword(null!, storedHash, clientHashedPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}
