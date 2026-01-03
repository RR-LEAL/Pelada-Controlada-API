namespace PeladaControladaAPI.DTOs;

public class ExibirUsuarioDto
{
    public int Id { get; set; }
    public string? NomeUsuario { get; set; }
    public string? Email { get; set; }
    public DateOnly? DataCadastro { get; set; }
}