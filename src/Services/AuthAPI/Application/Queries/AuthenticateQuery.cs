using Bismuth.Domain.Entities;
using MediatR;

namespace AuthAPI.Application.Queries;

public record AuthenticateQuery(
    string Email,
    string Password) : IRequest<User>;