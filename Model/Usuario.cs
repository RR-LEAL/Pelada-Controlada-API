using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("usuario")]
        public string? NomeUsuario { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("senha")]
        public string? Senha { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; } // Ex: 5511999998888

        [Column("data_cadastro")]
        public DateOnly? DataCadastro { get; set; } // DATE vira DateOnly no .NET moderno
        [Column("codigo_recuperacao")]
        public string? CodigoRecuperacao { get; set; } // O código de 6 dígitos

        [Column("validade_codigo")]
        public DateTime? ValidadeCodigo { get; set; } // Para o código expirar em 10 min
    }
}