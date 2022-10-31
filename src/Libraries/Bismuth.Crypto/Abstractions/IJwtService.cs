using System.Security.Claims;

namespace Bismuth.Crypto.Abstractions;

public interface IJwtService
{
    string CreateJwt(string secret, IList<Claim> claims, DateTime expirationTimeStamp);
}