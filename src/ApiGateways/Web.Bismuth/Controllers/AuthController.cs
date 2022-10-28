using System.Net;
using Bismuth.Contracts.v1.Auth;
using Bismuth.Contracts.v1.Session;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Bismuth.Application.Commands.Auth;

namespace Web.Bismuth.Controllers;

[Route("")]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("sign-in")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(SignInResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SignInAsync(
        SignInRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<SignInCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (result is null)
        {
            return Unauthorized("Invalid email or password.");
        }

        // TODO: read refresh token and add to cookies

        return Ok(new SignInResponse(result.IdToken));
    }
}