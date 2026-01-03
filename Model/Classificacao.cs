using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_classificacao")]
    public class Classificacao
    {
        [Key]
        [Column("id_classificacao")]
        public int Id { get; set; }

        [Column("id_campeonato")]
        public int IdCampeonato { get; set; }
        [ForeignKey("IdCampeonato")]
        public virtual Campeonato? Campeonato { get; set; }

        [Column("id_time")]
        public int IdTime { get; set; }
        [ForeignKey("IdTime")]
        public virtual Time? Time { get; set; }

        [Column("pontos")] public int Pontos { get; set; }
        [Column("vitorias")] public int Vitorias { get; set; }
        [Column("empates")] public int Empates { get; set; }
        [Column("derrotas")] public int Derrotas { get; set; }
        [Column("golspro")] public int GolsPro { get; set; }
        [Column("golscontra")] public int GolsContra { get; set; }
        [Column("salgogols")] public int SaldoGols { get; set; } // "salgogols" mantido como no diagrama
        [Column("jogos")] public int Jogos { get; set; }
        [Column("grupo")] public int GrupoNumero { get; set; }
    }
}