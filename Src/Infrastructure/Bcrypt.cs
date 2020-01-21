using System;
using BCrypt.Net;

namespace AperoBoxApi.Infrastructure
{
    public class Bcrypt
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string passwordHash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(passwordHash, password);
        }
    }
}