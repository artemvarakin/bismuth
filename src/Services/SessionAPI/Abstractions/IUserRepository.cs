using Bismuth.Domain.Entities;

namespace SessionAPI.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken token);
}