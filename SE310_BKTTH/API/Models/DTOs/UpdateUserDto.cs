using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UpdateUserDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Role { get; set; }
}
