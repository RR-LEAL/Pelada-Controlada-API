using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_times")]
    public class Time
    {
        [Key]
        [Column("id_time")]
        public int Id { get; set; }

        [Column("id_campeonato")]
        public int IdCampeonato { get; set; }
        [ForeignKey("IdCampeonato")]
        public virtual Campeonato? Campeonato { get; set; }

        [Column("id_equipe")]
        public int IdEquipe { get; set; }
        [ForeignKey("IdEquipe")]
        public virtual Equipe? Equipe { get; set; }

        [Column("id_grupo")]
        public int? IdGrupo { get; set; }
        [ForeignKey("IdGrupo")]
        public virtual Grupo? Grupo { get; set; }

        [Column("posicao_final")] public int PosicaoFinal { get; set; }
        [Column("qtd_jogos")] public int QtdJogos { get; set; }
        [Column("vitorias")] public int Vitorias { get; set; }
        [Column("empates")] public int Empates { get; set; }
        [Column("derrotas")] public int Derrotas { get; set; }
        [Column("golspro")] public int GolsPro { get; set; }
        [Column("golscontra")] public int GolsContra { get; set; }
        [Column("melhor_ataque")] public bool MelhorAtaque { get; set; }
        [Column("melhor_defesa")] public bool MelhorDefesa { get; set; }
        [Column("media_gols")] public double MediaGols { get; set; }
        [Column("nivel")] public int Nivel { get; set; }
    }
}