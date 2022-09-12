using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.Message);
    }
}