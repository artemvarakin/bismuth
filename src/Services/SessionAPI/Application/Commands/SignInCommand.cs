using Bismuth.Domain.Entities;
using MediatR;

namespace SessionAPI.Application.Commands;

public record SignInCommand(
    string Email,
    string Password) : IRequest<User>;