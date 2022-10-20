using Bismuth.Domain.Entities;
using MediatR;

namespace UserAPI.Application.Queries;

public record GetUserByEmailQuery(string Email) : IRequest<User>;