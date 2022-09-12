using Bismuth.Domain.Entities;
using BismuthAPI.Contracts.User;

namespace BismuthAPI.Abstractions.Services;

// TODO: add documentation
public interface IUserManagerService
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken token);
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, CancellationToken token);
}