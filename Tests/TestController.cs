using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Tests;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new System.Exception("Test exception");
    }
}