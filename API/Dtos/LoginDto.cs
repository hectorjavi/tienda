using System.ComponentModel.DataAnnotations;
namespace API.Dtos;

//Atajo: dtolog

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
