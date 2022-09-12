using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[Authorize]
public sealed class UserController : ApiController
{
    private readonly IUserManagerService _userManager;

    public UserController(IUserManagerService userManager)
    {
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserRequest request, CancellationToken token)
    {
        var user = await _userManager.GetUserByEmailAsync(request.Email, token);
        if (user is not null)
        {
            return Conflict("User already registered.");
        }

        var registeredUser = await _userManager.RegisterUserAsync(request, token);

        return Ok(registeredUser);
    }
}