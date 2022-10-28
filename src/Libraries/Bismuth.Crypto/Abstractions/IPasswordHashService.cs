namespace Bismuth.Crypto.Abstractions;

// TODO: add docs
public interface IPasswordHashService
{
    string CreatePasswordHash(string password);
    bool IsPasswordValid(string passwordHash, string password);
}