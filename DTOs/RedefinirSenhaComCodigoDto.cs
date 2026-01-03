using System.ComponentModel.DataAnnotations;

namespace PeladaControladaAPI.DTOs;

public class RedefinirSenhaComCodigoDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string NovaSenha { get; set; } = string.Empty;
}