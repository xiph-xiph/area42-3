using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Backend_Area42_3.Services;

public class Argon2PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 4,
            MemorySize = 65536, // 64 MB
            Iterations = 3,
        };

        byte[] hash = argon2.GetBytes(32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split('.');
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 4,
            MemorySize = 65536,
            Iterations = 3,
        };

        byte[] computed = argon2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, computed);
    }
}
