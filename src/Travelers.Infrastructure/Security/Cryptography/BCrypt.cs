using Travelers.Domain.Interfaces.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace Travelers.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncryptor
{
    public string Encrypt(string password)
    {
        return BC.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}