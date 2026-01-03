using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_equipes")]
    public class Equipe
    {
        [Key]
        [Column("id_equipe")]
        public int Id { get; set; }

        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual CategoriaEquipe? Categoria { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("logo")]
        public string? Logo { get; set; }

        [Column("sigla")]
        public string? Sigla { get; set; }

        [Column("nivel")]
        public int Nivel { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }
    }
}