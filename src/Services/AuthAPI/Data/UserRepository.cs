using Bismuth.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AuthAPI.Abstractions;
using AuthAPI.Configurations;

namespace AuthAPI.Data;

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

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token)
        => await GetUsersCollection().Find(u => u.Email == email).FirstOrDefaultAsync(token);

    private IMongoCollection<User> GetUsersCollection()
    {
        var db = _client.GetDatabase(_configuration.DatabaseName);
        return db.GetCollection<User>(_configuration.UsersCollectionName);
    }
}
