using System.Net;
using Bismuth.Contracts.v1.User;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Bismuth.Application.Commands.User;
using Web.Bismuth.Application.Common.User;
using Web.Bismuth.Application.Queries.User;

namespace Web.Bismuth.Controllers;

public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterUserAsync(
        RegisterUserRequest request,
        CancellationToken token)
    {
        var query = new GetUserByEmailQuery(request.Email);
        if (await _mediator.Send(query, token) is not null)
        {
            return Conflict($"User with email {request.Email} already registered.");
        }

        var command = _mapper.Map<RegisterUserCommand>(request);
        var result = await _mediator.Send(command, token);

        return Ok(_mapper.Map<RegisterUserResponse>(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken token)
    {
        var query = new GetUserByIdQuery(id);
        if (await _mediator.Send(query, token) is not GetUserResult result)
        {
            return NotFound("User not found.");
        }

        return Ok(_mapper.Map<GetUserResponse>(result));
    }
}