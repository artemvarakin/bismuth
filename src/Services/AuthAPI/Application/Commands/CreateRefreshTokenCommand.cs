using Bismuth.Domain.Entities;
using MediatR;

namespace AuthAPI.Application.Commands;

public record CreateRefreshTokenCommand(User User) : IRequest<string>;