using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_campeonatos")]
    public class Campeonato
    {
        [Key]
        [Column("id_campeonato")]
        public int Id { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }

        [Column("id_tipo")]
        public int IdTipo { get; set; }
        [ForeignKey("IdTipo")]
        public virtual TipoCampeonato? Tipo { get; set; }

        [Column("id_fase")]
        public int IdFase { get; set; }
        [ForeignKey("IdFase")]
        public virtual Fase? Fase { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("data_inicio")]
        public DateOnly? DataInicio { get; set; }

        [Column("data_fim")]
        public DateOnly? DataFim { get; set; }

        [Column("disputa_terceiro_lugar")] public bool DisputaTerceiroLugar { get; set; }
        [Column("disputa_em_dupla")] public bool DisputaEmDupla { get; set; }
        [Column("ida_e_volta")] public bool IdaEVolta { get; set; }
        [Column("trofeu")] public string? Trofeu { get; set; }

        // Flags de controle
        [Column("salvo")] public bool Salvo { get; set; }
        [Column("criado_auto")] public bool CriadoAuto { get; set; }
        [Column("partida_final")] public bool PartidaFinal { get; set; }
        [Column("data_criado")] public DateTime? DataCriado { get; set; }
        [Column("data_salvo")] public DateTime? DataSalvo { get; set; }
    }
}