using System.ComponentModel.DataAnnotations;

namespace PeladaControladaAPI.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string Senha { get; set; } = string.Empty;
}