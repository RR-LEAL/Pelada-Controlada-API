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
        public required string Email { get; set; }

        [Column("senha")]
        public string? Senha { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; } // Ex: 5511999998888

        [Column("data_cadastro")]
        public DateOnly? DataCadastro { get; set; } // DATE vira DateOnly no .NET moderno

    }
}