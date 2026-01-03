using System.ComponentModel.DataAnnotations;

namespace PeladaControladaAPI.DTOs;

public class CriarUsuarioDto
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O formato do email é inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;
}