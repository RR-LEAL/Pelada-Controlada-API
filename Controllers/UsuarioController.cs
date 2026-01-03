using Microsoft.AspNetCore.Mvc;
using PeladaControladaAPI.Data;
using PeladaControladaAPI.DTOs;
using PeladaControladaAPI.model;
using PeladaControladaAPI.Services;

namespace PeladaControladaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMensagemService _mensagemService; 

    // Injeção de dependência do Banco de Dados
    public UsuarioController(AppDbContext context, IMensagemService mensagemService)
    {
        _context = context;
        _mensagemService = mensagemService;
    }

    [HttpPost]
    public IActionResult CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        var emailExiste = _context.Usuarios.Any(u => u.Email == dto.Email);
        if (emailExiste)
        {
            return BadRequest("Este email já está cadastrado.");
        }

        //Criptografa a senha (Hash)
        string senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var novoUsuario = new Usuario
        {
            NomeUsuario = dto.NomeUsuario,
            Email = dto.Email,
            Senha = senhaHash,
            Telefone = dto.Telefone,
            DataCadastro = DateOnly.FromDateTime(DateTime.Now)
        };

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        var usuarioRetorno = new ExibirUsuarioDto
        {
            Id = novoUsuario.Id,
            NomeUsuario = novoUsuario.NomeUsuario,
            Email = novoUsuario.Email,
            Telefone = novoUsuario.Telefone,
            DataCadastro = novoUsuario.DataCadastro
        };

        return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = novoUsuario.Id }, usuarioRetorno);
    }

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
            Telefone = usuario.Telefone,
            DataCadastro = usuario.DataCadastro
        };

        return Ok(usuarioRetorno);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

        if (usuario == null)
        {
            return Unauthorized("Email ou senha inválidos.");
        }

        bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

        if (!senhaValida)
        {
            return Unauthorized("Email ou senha inválidos.");
        }

        return Ok(new
        {
            mensagem = "Login realizado com sucesso!",
            usuario = usuario.NomeUsuario,
            id = usuario.Id
        });
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarUsuario(int id, [FromBody] AtualizarUsuarioDto dto)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        if (!string.IsNullOrEmpty(dto.NomeUsuario))
        {
            usuario.NomeUsuario = dto.NomeUsuario;
        }

        if (!string.IsNullOrEmpty(dto.Email))
        {
            if (usuario.Email != dto.Email) 
            {
                var emailEmUso = _context.Usuarios.Any(u => u.Email == dto.Email && u.Id != id);
                if (emailEmUso)
                {
                    return BadRequest("Este e-mail já está sendo utilizado por outro usuário.");
                }
                usuario.Email = dto.Email;
            }
        }

        if (!string.IsNullOrEmpty(dto.Telefone))
        {
            usuario.Telefone = dto.Telefone;
        }

        usuario.NomeUsuario = dto.NomeUsuario;
        usuario.Email = dto.Email;
        usuario.Telefone = dto.Telefone;

        _context.SaveChanges();

        return Ok(new ExibirUsuarioDto
        {
            Id = usuario.Id,
            NomeUsuario = usuario.NomeUsuario,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            DataCadastro = usuario.DataCadastro
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarUsuario(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        try
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Não é possível excluir este usuário pois ele possui dados vinculados (campeonatos, jogadores, etc).");
        }
    }

    [HttpPatch("{id}/alterar-senha")]
    public IActionResult AlterarSenha(int id, [FromBody] AlterarSenhaDto dto)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        bool senhaAtualConfere = BCrypt.Net.BCrypt.Verify(dto.SenhaAtual, usuario.Senha);
        if (!senhaAtualConfere)
        {
            return BadRequest("A senha atual está incorreta.");
        }

        string novaSenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

        usuario.Senha = novaSenhaHash;
        _context.SaveChanges();

        return Ok(new { mensagem = "Senha alterada com sucesso!" });
    }

    [HttpPost("enviar-codigo-recuperacao")]
    public async Task<IActionResult> SolicitarRecuperacao([FromBody] SolicitarRecuperacaoDto dto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);
        if (usuario == null) return NotFound("Email não encontrado.");

        if (string.IsNullOrEmpty(usuario.Telefone))
            return BadRequest("Este usuário não possui telefone cadastrado para recuperação via WhatsApp.");

        // Gera código de 6 dígitos aleatórios
        string codigo = Random.Shared.Next(100000, 999999).ToString();

        // Salva no banco com validade de 15 minutos
        usuario.CodigoRecuperacao = codigo;
        usuario.ValidadeCodigo = DateTime.Now.AddMinutes(5);
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();

        // Envia via "WhatsApp" (Simulado no Console por enquanto)
        string mensagem = $"Seu código de recuperação Pelada Controlada é: {codigo}";
        await _mensagemService.EnviarWhatsAppAsync(usuario.Telefone, mensagem);

        return Ok(new { mensagem = "Código de recuperação enviado para o WhatsApp cadastrado." });
    }

    // 2. Endpoint para TROCAR A SENHA usando o código
    [HttpPost("redefinir-senha")]
    public IActionResult RedefinirSenha([FromBody] RedefinirSenhaComCodigoDto dto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        // Validações do Código
        if (usuario.CodigoRecuperacao != dto.Codigo)
            return BadRequest("Código inválido.");

        if (usuario.ValidadeCodigo < DateTime.Now)
            return BadRequest("O código expirou. Solicite um novo.");

        // Se passou, troca a senha
        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

        // Limpa o código para não ser usado de novo
        usuario.CodigoRecuperacao = null;
        usuario.ValidadeCodigo = null;

        _context.SaveChanges();

        return Ok(new { mensagem = "Senha redefinida com sucesso! Agora você pode fazer login." });
    }
}