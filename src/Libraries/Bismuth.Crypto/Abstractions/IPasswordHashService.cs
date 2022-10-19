using Bismuth.Domain.Entities;

namespace Bismuth.Crypto.Abstractions;

// TODO: add docs
public interface IPasswordHashService
{
    string CreatePasswordHash(string password);
    bool IsPasswordValid(User user, string password);
}