using MediatR;
using Web.Bismuth.Application.Common.User;

namespace Web.Bismuth.Application.Queries.User;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserResult>;