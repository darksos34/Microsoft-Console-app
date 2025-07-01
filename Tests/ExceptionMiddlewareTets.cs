using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ConsoleApp1.Tests;

public class ExceptionMiddlewareTests : IClassFixture<WebApplicationFactory<ConsoleApp1.Exception.ExceptionMiddleware>>
{
    private readonly WebApplicationFactory<ConsoleApp1.Exception.ExceptionMiddleware> _factory;

    public ExceptionMiddlewareTests(WebApplicationFactory<ConsoleApp1.Exception.ExceptionMiddleware> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExceptionMiddleware_ReturnsInternalServerError()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/Test");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("interne serverfout", content);
    }
}