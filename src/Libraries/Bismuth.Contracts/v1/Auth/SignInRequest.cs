namespace Bismuth.Contracts.v1.Session;

public record SignInRequest(
    string Email,
    string Password);