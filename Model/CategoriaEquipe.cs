using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_categs_equipe")]
    public class CategoriaEquipe
    {
        [Key]
        [Column("id_categoria")]
        public int Id { get; set; }

        [Column("id_tipo")]
        public int IdTipo { get; set; }

        [ForeignKey("IdTipo")]
        public virtual TipoEquipe? TipoEquipe { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("pasta")]
        public string? Pasta { get; set; }
    }
}