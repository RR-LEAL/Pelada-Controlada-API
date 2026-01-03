using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_fases")]
    public class Fase
    {
        [Key]
        [Column("id_fase")]
        public int Id { get; set; }

        [Column("descricao")]
        public string? Descricao { get; set; }
    }
}