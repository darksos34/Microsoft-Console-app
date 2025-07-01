using ConsoleApp1.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Production-specific config
}
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ConsoleApp1.Exception.ExceptionMiddleware>();
app.UseExceptionHandler("/error");

app.MapControllers();

await app.RunAsync();

