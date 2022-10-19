namespace Bismuth.Contracts.v1.User;

public sealed record RegisterUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);