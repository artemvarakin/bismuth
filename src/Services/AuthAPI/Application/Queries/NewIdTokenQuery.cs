using Bismuth.Domain.Entities;
using MediatR;

namespace AuthAPI.Application.Queries;

public record NewIdTokenQuery(User User) : IRequest<string>;