using MediatR;
using Web.Bismuth.Application.Common;

namespace Web.Bismuth.Application.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserResult>;