using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Model;

public class UserDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Dit veld is verplicht.")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [StringLength(50)]
    public string? Username { get; set; }

}