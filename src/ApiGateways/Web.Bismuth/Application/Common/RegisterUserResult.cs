namespace Web.Bismuth.Application.Common;

public record RegisterUserResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);