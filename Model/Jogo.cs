using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_jogos")]
    public class Jogo
    {
        [Key]
        [Column("id_jogo")]
        public int Id { get; set; }

        [Column("id_campeonato")]
        public int IdCampeonato { get; set; }
        [ForeignKey("IdCampeonato")]
        public virtual Campeonato? Campeonato { get; set; }

        [Column("id_fase")]
        public int IdFase { get; set; }
        [ForeignKey("IdFase")]
        public virtual Fase? Fase { get; set; }

        [Column("id_time1")]
        public int IdTime1 { get; set; }
        [ForeignKey("IdTime1")]
        public virtual Time? Time1 { get; set; } // Apenas a ForeignKey basta

        [Column("id_time2")]
        public int IdTime2 { get; set; }
        [ForeignKey("IdTime2")]
        public virtual Time? Time2 { get; set; } // Apenas a ForeignKey basta

        [Column("rodada")] public int Rodada { get; set; }
        [Column("numero")] public int Numero { get; set; }

        [Column("gol_time1")] public int GolTime1 { get; set; }
        [Column("gol_time2")] public int GolTime2 { get; set; }
        [Column("penalti")] public int Penalti { get; set; }
        [Column("grupo")] public int Grupo { get; set; }

        [Column("salvo")] public bool Salvo { get; set; }
        [Column("criado_auto")] public bool CriadoAuto { get; set; }
        [Column("partida_final")] public bool PartidaFinal { get; set; }
        [Column("data_criado")] public DateTime? DataCriado { get; set; }
        [Column("data_salvo")] public DateTime? DataSalvo { get; set; }
    }
}