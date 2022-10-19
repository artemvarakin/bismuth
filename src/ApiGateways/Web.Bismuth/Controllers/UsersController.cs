using System.Net;
using Bismuth.Contracts.v1.User;
using Microsoft.AspNetCore.Mvc;
using Web.Bismuth.Abstractions;

namespace Web.Bismuth.Controllers;

public class UsersController : ApiController
{
    private readonly IUserManager _userManager;

    public UsersController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(
        RegisterUserRequest request,
        CancellationToken token)
    {
        if (await _userManager.GetUserByEmailAsync(request.Email, token) is not null)
        {
            return Conflict($"User with email {request.Email} already registered.");
        }

        return await _userManager.RegisterUserAsync(request, token);
    }
}