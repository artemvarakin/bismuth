namespace Web.Bismuth.Application.Common.Auth;

public record SignInResult(
    string IdToken,
    string RefreshToken);