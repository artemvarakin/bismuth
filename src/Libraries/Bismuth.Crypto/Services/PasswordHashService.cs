using System.Security.Cryptography;
using Bismuth.Crypto.Abstractions;
using Bismuth.Domain.Entities;

namespace Bismuth.Crypto.Services;

public sealed class PasswordHashService : IPasswordHashService
{
    private const string _salt = "bZ9fkESnj7jlMRjNo+1jiIl2128iToNwh9rSQdypQsmjNn7c66sOdQ==";

    /// <inheritdoc />
    public string CreatePasswordHash(string password)
    {
        var hash = ComputeHash(password);
        return GetString(hash);
    }

    /// <inheritdoc />
    public bool IsPasswordValid(User user, string password)
    {
        var hash = ComputeHash(password);
        return user.PasswordHash.SequenceEqual(GetString(hash));
    }

    private static byte[] ComputeHash(string password)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(_salt));
        return hmac.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password));
    }

    private static string GetString(byte[] hash)
        => Convert.ToBase64String(hash)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", string.Empty);
}