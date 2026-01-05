using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeladaControladaAPI.Data;
using PeladaControladaAPI.DTOs;
using PeladaControladaAPI.model;
using PeladaControladaAPI.Model;
using PeladaControladaAPI.Services;
using Serilog;

namespace PeladaControladaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMensagemService _mensagemService;
    private readonly ILogger<UsuarioController> _logger;

    // Injeção de dependência do Banco de Dados
    public UsuarioController(AppDbContext context, IMensagemService mensagemService, ILogger<UsuarioController> logger)
    {
        _context = context;
        _mensagemService = mensagemService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        try
        {
            _logger.LogInformation("📝 Iniciando criação de novo usuário com email: {Email}", dto.Email);

            var emailExiste = _context.Usuarios.Any(u => u.Email == dto.Email);
            if (emailExiste)
            {
                _logger.LogWarning("⚠️ Tentativa de criar usuário com email já existente: {Email}", dto.Email);
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

            _logger.LogInformation("✅ Usuário criado com sucesso. ID: {UsuarioId}, Email: {Email}", novoUsuario.Id, novoUsuario.Email);

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao criar usuário com email: {Email}", dto.Email);
            return StatusCode(500, "Erro ao criar usuário.");
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarUsuarioPorId(int id)
    {
        try
        {
            _logger.LogInformation("🔍 Buscando usuário com ID: {UsuarioId}", id);

            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Usuário não encontrado. ID: {UsuarioId}", id);
                return NotFound("Usuário não encontrado.");
            }

            _logger.LogInformation("✅ Usuário encontrado. ID: {UsuarioId}, Email: {Email}", usuario.Id, usuario.Email);

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao buscar usuário com ID: {UsuarioId}", id);
            return StatusCode(500, "Erro ao buscar usuário.");
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        try
        {
            _logger.LogInformation("🔐 Tentativa de login com email: {Email}", dto.Email);

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Tentativa de login com email não encontrado: {Email}", dto.Email);
                return Unauthorized("Email ou senha inválidos.");
            }

            bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

            if (!senhaValida)
            {
                _logger.LogWarning("⚠️ Tentativa de login com senha inválida para email: {Email}", dto.Email);
                return Unauthorized("Email ou senha inválidos.");
            }

            _logger.LogInformation("✅ Login bem-sucedido. Usuário ID: {UsuarioId}, Email: {Email}", usuario.Id, usuario.Email);

            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                usuario = usuario.NomeUsuario,
                id = usuario.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao fazer login com email: {Email}", dto.Email);
            return StatusCode(500, "Erro ao fazer login.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarUsuario(int id, [FromBody] AtualizarUsuarioDto dto)
    {
        try
        {
            _logger.LogInformation("🔄 Iniciando atualização do usuário ID: {UsuarioId}", id);

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Tentativa de atualizar usuário não encontrado. ID: {UsuarioId}", id);
                return NotFound("Usuário não encontrado.");
            }

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
                        _logger.LogWarning("⚠️ Email já em uso por outro usuário: {Email}", dto.Email);
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

            _logger.LogInformation("✅ Usuário atualizado com sucesso. ID: {UsuarioId}", id);

            return Ok(new ExibirUsuarioDto
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                DataCadastro = usuario.DataCadastro
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao atualizar usuário ID: {UsuarioId}", id);
            return StatusCode(500, "Erro ao atualizar usuário.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarUsuario(int id)
    {
        try
        {
            _logger.LogInformation("🗑️ Iniciando exclusão do usuário ID: {UsuarioId}", id);

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Tentativa de deletar usuário não encontrado. ID: {UsuarioId}", id);
                return NotFound("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            _logger.LogInformation("✅ Usuário deletado com sucesso. ID: {UsuarioId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao deletar usuário ID: {UsuarioId}", id);
            return BadRequest("Não é possível excluir este usuário pois ele possui dados vinculados (campeonatos, jogadores, etc).");
        }
    }

    [HttpPatch("{id}/alterar-senha")]
    public IActionResult AlterarSenha(int id, [FromBody] AlterarSenhaDto dto)
    {
        try
        {
            _logger.LogInformation("🔐 Iniciando alteração de senha do usuário ID: {UsuarioId}", id);

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Tentativa de alterar senha de usuário não encontrado. ID: {UsuarioId}", id);
                return NotFound("Usuário não encontrado.");
            }

            bool senhaAtualConfere = BCrypt.Net.BCrypt.Verify(dto.SenhaAtual, usuario.Senha);
            if (!senhaAtualConfere)
            {
                _logger.LogWarning("⚠️ Tentativa de alterar senha com senha atual incorreta. ID: {UsuarioId}", id);
                return BadRequest("A senha atual está incorreta.");
            }

            string novaSenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

            usuario.Senha = novaSenhaHash;
            _context.SaveChanges();

            _logger.LogInformation("✅ Senha alterada com sucesso. ID: {UsuarioId}", id);
            return Ok(new { mensagem = "Senha alterada com sucesso!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao alterar senha do usuário ID: {UsuarioId}", id);
            return StatusCode(500, "Erro ao alterar senha.");
        }
    }

    [HttpPost("recuperar-senha")]
    public async Task<IActionResult> SolicitarRecuperacao(
    [FromBody] SolicitarRecuperacaoDto dto)
    {
        try
        {
            _logger.LogInformation("📧 Solicitação de recuperação de senha para email: {Email}", dto.Email);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Solicitação de recuperação para email não encontrado: {Email}", dto.Email);
                return Ok(); // não revela se o email existe
            }

            if (!await PodeGerarOtp(usuario.Id))
            {
                _logger.LogWarning("⚠️ Muitas tentativas de recuperação. Usuário ID: {UsuarioId}", usuario.Id);
                return BadRequest("Muitas tentativas. Tente novamente mais tarde.");
            }

            var (codigo, otpGerado) = OtpService.Gerar();

            var otp = new UsuarioCodigoOtp
            {
                Usuario = usuario,
                Hash = otpGerado.Hash,
                ExpiraEm = otpGerado.ExpiraEm
            };

            _context.UsuarioCodigosOtp.Add(otp);
            await _context.SaveChangesAsync();

            await _mensagemService.EnviarAsync(
                usuario.Email,
                $"Recuperação de senha - Seu código de verificação é {codigo}"
            );

            _logger.LogInformation("✅ Código OTP gerado e enviado para email: {Email}", usuario.Email);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao solicitar recuperação de senha para email: {Email}", dto.Email);
            return StatusCode(500, "Erro ao solicitar recuperação de senha.");
        }
    }

    private async Task<bool> PodeGerarOtp(int usuarioId)
    {
        var limite = DateTime.UtcNow.AddMinutes(-15);

        var tentativas = await _context.UsuarioCodigosOtp
            .CountAsync(o =>
                o.Usuario.Id == usuarioId &&
                o.CriadoEm >= limite
            );

        return tentativas < 3;
    }


    [HttpPost("validar-codigo")]
    public async Task<IActionResult> ValidarCodigo(
    [FromBody] ValidarCodigoDto dto)
    {
        try
        {
            _logger.LogInformation("🔐 Tentativa de validação de código OTP para email: {Email}", dto.Email);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
            {
                _logger.LogWarning("⚠️ Tentativa de validar código para email não encontrado: {Email}", dto.Email);
                return BadRequest("Código inválido.");
            }

            var otp = await _context.UsuarioCodigosOtp
                .Where(o =>
                    o.Usuario.Id == usuario.Id &&
                    !o.Usado &&
                    o.ExpiraEm > DateTime.UtcNow
                )
                .OrderByDescending(o => o.CriadoEm)
                .FirstOrDefaultAsync();

            if (otp == null)
            {
                _logger.LogWarning("⚠️ Código OTP inválido ou expirado para email: {Email}", dto.Email);
                return BadRequest("Código inválido ou expirado.");
            }

            var valido = OtpService.Validar(dto.Codigo, otp);

            if (!valido)
            {
                _logger.LogWarning("⚠️ Código OTP inválido para email: {Email}", dto.Email);
                return BadRequest("Código inválido.");
            }

            // Deletar o código usado e todos os outros não usados do mesmo usuário
            var codigosDoUsuario = await _context.UsuarioCodigosOtp
                .Where(o => o.Usuario.Id == usuario.Id)
                .ToListAsync();

            _context.UsuarioCodigosOtp.RemoveRange(codigosDoUsuario);
            await _context.SaveChangesAsync();

            _logger.LogInformation("✅ Código OTP validado com sucesso para email: {Email}", dto.Email);

            return Ok("Código validado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Erro ao validar código OTP para email: {Email}", dto.Email);
            return StatusCode(500, "Erro ao validar código.");
        }
    }

}
