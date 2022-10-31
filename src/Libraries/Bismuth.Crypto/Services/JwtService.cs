using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bismuth.Crypto.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace Bismuth.Crypto.Services;

public class JwtService : IJwtService
{
    public string CreateJwt(
        string secret,
        IList<Claim> claims,
        DateTime expirationTimeStamp)
    {
        var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret));
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256Signature);

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            expires: expirationTimeStamp,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);
    }
}