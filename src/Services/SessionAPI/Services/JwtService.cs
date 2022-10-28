using Bismuth.Domain.Entities;
using SessionAPI.Abstractions;

namespace SessionAPI.Services;

public class JwtService : IJwtService
{
    public async Task<(string idToken, string refreshToken)> CreateNewTokenPairAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}