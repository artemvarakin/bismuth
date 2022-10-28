namespace Web.Bismuth.Application.Common.User;

public record RegisterUserResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);