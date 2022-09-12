using Bismuth.Domain.Entities;

namespace BismuthAPI.Abstractions.Services;

// TODO: add documentation
public interface IPasswordHashService
{
    string CreatePasswordHash(string password);
    bool IsPasswordValid(User user, string password);
}