using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_tipos_campeonato")]
    public class TipoCampeonato
    {
        [Key]
        [Column("id_tipo")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("descricao")]
        public string? Descricao { get; set; } 
    }
}