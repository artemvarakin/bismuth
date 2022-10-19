namespace Bismuth.Contracts.v1.User;

public sealed record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);