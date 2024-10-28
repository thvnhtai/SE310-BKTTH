using System.ComponentModel.DataAnnotations;

namespace API.Models.Entity;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public byte[] PasswordHash { get; set; }
    [Required]
    public byte[] PasswordSalt { get; set; }
    [Required]
    public string Role { get; set; }
}
