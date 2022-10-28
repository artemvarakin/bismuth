using MediatR;
using Web.Bismuth.Application.Common.User;

namespace Web.Bismuth.Application.Queries.User;

public record GetUserByEmailQuery(string Email) : IRequest<GetUserResult>;