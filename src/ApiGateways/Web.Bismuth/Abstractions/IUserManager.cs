using Bismuth.Contracts.v1.User;

namespace Web.Bismuth.Abstractions;

// TODO: add docs
public interface IUserManager
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, CancellationToken token);
    Task<GetUserResponse?> GetUserByEmailAsync(string email, CancellationToken token);
    // Task<GetUserResponse> GetUserByIdAsync(Guid id, CancellationToken token);
}