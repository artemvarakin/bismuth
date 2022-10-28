using Bismuth.Domain.Entities;

namespace AuthAPI.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken token);
}