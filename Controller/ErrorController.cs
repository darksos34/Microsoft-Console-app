namespace ConsoleApp1.Controller;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult Error() => Problem();
}