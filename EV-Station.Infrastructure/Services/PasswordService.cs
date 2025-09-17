using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EV_Station.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(null!, hashedPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
