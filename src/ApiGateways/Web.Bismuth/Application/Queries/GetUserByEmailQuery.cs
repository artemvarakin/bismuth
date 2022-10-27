using MediatR;
using Web.Bismuth.Application.Common;

namespace Web.Bismuth.Application.Queries;

public record GetUserByEmailQuery(string Email) : IRequest<GetUserResult>;