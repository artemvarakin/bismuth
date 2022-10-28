using MediatR;
using Web.Bismuth.Application.Common.Auth;

namespace Web.Bismuth.Application.Commands.Auth;

public record SignInCommand(
    string Email,
    string Password) : IRequest<SignInResult>;