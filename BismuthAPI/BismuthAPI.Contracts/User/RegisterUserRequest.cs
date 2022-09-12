namespace BismuthAPI.Contracts.User;

public sealed record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);