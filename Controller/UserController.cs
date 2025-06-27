using ConsoleApp1.Data;
using ConsoleApp1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Controller;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> Get()
    {
        var users = await _db.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Username = u.Username
            })
            .ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Username = dto.Username
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        var resultDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Username = user.Username
        };
        return CreatedAtAction(nameof(Get), new { id = user.Id }, resultDto);
    }
}