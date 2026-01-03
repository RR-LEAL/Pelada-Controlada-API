using Microsoft.AspNetCore.Mvc;
using PeladaControladaAPI.Data;
using PeladaControladaAPI.DTOs;
using PeladaControladaAPI.model;

namespace PeladaControladaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    // Injeção de dependência do Banco de Dados
    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/usuario
    [HttpPost]
    public IActionResult CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        // 1. Verifica se o email já existe no banco
        var emailExiste = _context.Usuarios.Any(u => u.Email == dto.Email);
        if (emailExiste)
        {
            return BadRequest("Este email já está cadastrado.");
        }

        // 2. Criptografa a senha (Hash)
        string senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        // 3. Mapeia DTO para a Entidade do Banco (Model)
        var novoUsuario = new Usuario
        {
            NomeUsuario = dto.NomeUsuario,
            Email = dto.Email,
            Senha = senhaHash, // Salva o hash, nunca a senha pura
            DataCadastro = DateOnly.FromDateTime(DateTime.Now)
        };

        // 4. Salva no MySQL
        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        // 5. Prepara o retorno (sem a senha)
        var usuarioRetorno = new ExibirUsuarioDto
        {
            Id = novoUsuario.Id,
            NomeUsuario = novoUsuario.NomeUsuario,
            Email = novoUsuario.Email,
            DataCadastro = novoUsuario.DataCadastro
        };

        // Retorna status 201 (Created)
        return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = novoUsuario.Id }, usuarioRetorno);
    }

    // GET: api/usuario/{id}
    [HttpGet("{id}")]
    public IActionResult BuscarUsuarioPorId(int id)
    {
        var usuario = _context.Usuarios.Find(id);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        var usuarioRetorno = new ExibirUsuarioDto
        {
            Id = usuario.Id,
            NomeUsuario = usuario.NomeUsuario,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro
        };

        return Ok(usuarioRetorno);
    }
}