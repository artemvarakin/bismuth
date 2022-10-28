using Bismuth.Domain.Entities;

namespace AuthAPI.Abstractions;

public interface IJwtService
{
    Task<(string idToken, string refreshToken)> CreateNewTokenPairAsync(
        User user,
        CancellationToken cancellationToken);
}