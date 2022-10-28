using Bismuth.Domain.Entities;
using AuthAPI.Abstractions;

namespace AuthAPI.Services;

public class JwtService : IJwtService
{
    public async Task<(string idToken, string refreshToken)> CreateNewTokenPairAsync(User user, CancellationToken cancellationToken)
    {
        return ("new id token", "new refresh token");
    }
}