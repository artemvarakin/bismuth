using System.Security.Claims;
using AuthAPI.Configurations;
using Bismuth.Core.Identity;
using Bismuth.Crypto.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;

namespace AuthAPI.Application.Commands;

public class CreateRefreshTokenCommandHandler
    : IRequestHandler<CreateRefreshTokenCommand, string>
{
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly IJwtService _jwtService;

    public CreateRefreshTokenCommandHandler(
        IOptions<RefreshTokenSettings> refreshTokenSettings,
        IJwtService jwtService)
    {
        _jwtService = jwtService;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    public Task<string> Handle(
        CreateRefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var refreshTokenId = Guid.NewGuid();

        var claims = new List<Claim>
        {
            new Claim(nameof(BismuthClaimTypes.TokenId), refreshTokenId.ToString()),
            new Claim(nameof(BismuthClaimTypes.UserId), command.User.Id.ToString())
        };

        var expirationTimeStamp = DateTime.UtcNow
            .AddDays(_refreshTokenSettings.ExpirationPeriodInDays);

        var refreshToken = _jwtService.CreateJwt(
            secret: _refreshTokenSettings.Secret,
            claims: claims,
            expirationTimeStamp: expirationTimeStamp
        );

        // TODO: write refresh token to redis

        return Task.FromResult(refreshToken);
    }
}