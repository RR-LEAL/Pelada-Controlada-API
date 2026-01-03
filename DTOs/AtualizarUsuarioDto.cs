using System.ComponentModel.DataAnnotations;

namespace PeladaControladaAPI.DTOs;

public class AtualizarUsuarioDto
{
    public string NomeUsuario { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;
}