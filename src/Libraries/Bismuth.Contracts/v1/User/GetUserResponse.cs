namespace Bismuth.Contracts.v1.User;

public sealed record GetUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);