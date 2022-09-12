using Bismuth.Domain.Entities;
using BismuthAPI.Abstractions.Repositories;

namespace BismuthAPI.Data.Repositories;

public sealed class UserRepository : BaseDbRepository, IUserRepository
{
    public UserRepository(DataContext dataContext)
        : base(dataContext) { }

    /// <inheritdoc />
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token)
    => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, token);

    /// <inheritdoc />
    public async Task AddUserAsync(User user, CancellationToken token)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(token);
    }
}