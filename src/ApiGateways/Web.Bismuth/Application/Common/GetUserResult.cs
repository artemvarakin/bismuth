namespace Web.Bismuth.Application.Common;

public record GetUserResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);