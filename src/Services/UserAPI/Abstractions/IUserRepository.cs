using Bismuth.Domain.Entities;

namespace UserAPI.Abstractions;

public interface IUserRepository
{
    Task AddUserAsync(User user, CancellationToken token);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken token);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken token);
}