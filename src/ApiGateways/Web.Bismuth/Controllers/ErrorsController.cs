using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Bismuth.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return exception is ValidationException e
            ? ValidationProblem(e)
            : Problem(title: exception?.Message);
    }

    private IActionResult ValidationProblem(ValidationException e)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var err in e.Errors)
        {
            modelStateDictionary.AddModelError(
                err.PropertyName,
                err.ErrorMessage
            );
        }

        return ValidationProblem(modelStateDictionary);
    }
}