using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Model;

public class UserDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Dit veld is verplicht.")]
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
}