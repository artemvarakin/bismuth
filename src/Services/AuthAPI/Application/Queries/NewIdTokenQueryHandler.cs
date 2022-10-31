using System.Security.Claims;
using AuthAPI.Configurations;
using Bismuth.Core.Identity;
using Bismuth.Crypto.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;

namespace AuthAPI.Application.Queries;

public class NewIdTokenQueryHandler : IRequestHandler<NewIdTokenQuery, string>
{
    private readonly IdTokenSettings _idTokenConfiguration;
    private readonly IJwtService _jwtService;

    public NewIdTokenQueryHandler(
        IOptions<IdTokenSettings> options,
        IJwtService jwtService)
    {
        _idTokenConfiguration = options.Value;
        _jwtService = jwtService;
    }

    public Task<string> Handle(
        NewIdTokenQuery query,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(nameof(BismuthClaimTypes.UserId), query.User.Id.ToString()),
            new Claim(nameof(BismuthClaimTypes.FirstName), query.User.FirstName),
            new Claim(nameof(BismuthClaimTypes.LastName), query.User.LastName)
        };

        var expirationTimeStamp = DateTime.UtcNow
            .AddMinutes(_idTokenConfiguration.ExpirationPeriodInMinutes);

        var token = _jwtService.CreateJwt(
            secret: _idTokenConfiguration.Secret,
            claims: claims,
            expirationTimeStamp: expirationTimeStamp
        );

        return Task.FromResult(token);
    }
}