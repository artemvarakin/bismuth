namespace Web.Bismuth.Application.Common.User;

public record GetUserResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);