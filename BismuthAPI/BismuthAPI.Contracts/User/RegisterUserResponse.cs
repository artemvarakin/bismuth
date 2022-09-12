namespace BismuthAPI.Contracts.User;

public sealed record RegisterUserResponse(
    string FirstName,
    string LastName,
    string Email);