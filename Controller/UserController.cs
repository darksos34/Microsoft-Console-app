using ConsoleApp1.Data;
using ConsoleApp1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Controller;


/// <summary>
/// Handles user-related API operations.
/// </summary>
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    
    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>A list of user objects.</returns>
    /// <response code="200">Returns the list of users</response>
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsersAsList()
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

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="newUser">The user data to create.</param>
    /// <returns>The created user with assigned ID.</returns>
    /// <response code="201">Returns the newly created user</response>
    /// <response code="400">If the user data is invalid</response>
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto dto)
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
        return CreatedAtAction(nameof(GetUsersAsList), new { id = user.Id }, resultDto);
    }
}