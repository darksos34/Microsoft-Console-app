using ConsoleApp1.Data;
using ConsoleApp1.Model;
using Microsoft.AspNetCore.Http;
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
    /// <param name="dto">The user data to create.</param>
    /// <returns>The created user with assigned ID.</returns>
    /// <response code="201">Returns the newly created user</response>
    /// <response code="400">If the user data is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(dto.Name) ||
            string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Username))
        {
            return BadRequest(new { Error = "Name, Email, and Username are required." });
        }

        var user = new User
        {
            Name = dto.Name.Trim(),
            Email = dto.Email.Trim(),
            Username = dto.Username.Trim()
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

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, resultDto);
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user if found.</returns>
    /// <response code="200">Returns the user</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        var dto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Username = user.Username
        };

        return Ok(dto);
    }
}
