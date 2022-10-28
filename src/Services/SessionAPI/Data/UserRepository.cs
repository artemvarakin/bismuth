using Bismuth.Domain.Entities;
using SessionAPI.Abstractions;

namespace SessionAPI.Data;

public class UserRepository : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}