using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Exception;

[ApiController]
[Route("error")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult HandleError()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null)
        {
            var exception = exceptionHandlerPathFeature.Error;
            return Problem(detail: exception.Message, statusCode: 500);
        }
        return Problem(statusCode: 500);
    }
}