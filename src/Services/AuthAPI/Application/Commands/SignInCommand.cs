using Bismuth.Domain.Entities;
using MediatR;

namespace AuthAPI.Application.Commands;

public record SignInCommand(
    string Email,
    string Password) : IRequest<User>;