using Bismuth.Domain.Entities;
using MediatR;

namespace UserAPI;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string PasswordHash) : IRequest<User>;