using MediatR;
using Web.Bismuth.Application.Common.User;

namespace Web.Bismuth.Application.Commands.User;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<RegisterUserResult>;