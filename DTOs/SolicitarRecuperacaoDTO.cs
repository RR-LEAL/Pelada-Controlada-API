using System.ComponentModel.DataAnnotations;

namespace PeladaControladaAPI.DTOs;

public class SolicitarRecuperacaoDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}