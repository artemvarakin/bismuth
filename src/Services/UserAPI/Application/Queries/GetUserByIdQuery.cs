using Bismuth.Domain.Entities;
using MediatR;

namespace UserAPI.Application.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<User>;