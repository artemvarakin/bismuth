using Bismuth.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserAPI.Abstractions;
using UserAPI.Configurations;

namespace UserAPI.Data;

public class UserRepository : IUserRepository
{
    private readonly IMongoClient _client;
    private readonly MongoDbConnectionSettings _configuration;

    public UserRepository(
        IMongoClient client,
        IOptions<MongoDbConnectionSettings> options)
    {
        _client = client;
        _configuration = options.Value;
    }

    public async Task AddUserAsync(User user, CancellationToken token)
        => await GetUsersCollection()
            .InsertOneAsync(user, null, token);

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken token)
        => await GetUsersCollection()
            .Find(u => u.Id == id).FirstOrDefaultAsync(token);

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token)
        => await GetUsersCollection()
            .Find(u => u.Email == email).FirstOrDefaultAsync(token);

    public async Task<bool> IsUserExists(string email, CancellationToken token)
        => await GetUsersCollection()
            .Find(u => u.Email == email).CountDocumentsAsync(token) > 0;

    private IMongoCollection<User> GetUsersCollection()
    {
        var db = _client.GetDatabase(_configuration.DatabaseName);
        return db.GetCollection<User>(_configuration.UsersCollectionName);
    }
}