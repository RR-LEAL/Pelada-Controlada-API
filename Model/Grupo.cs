using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_grupos")]
    public class Grupo
    {
        [Key]
        [Column("id_grupo")]
        public int Id { get; set; }

        [Column("id_campeonato")]
        public int IdCampeonato { get; set; }
        [ForeignKey("IdCampeonato")]
        public virtual Campeonato? Campeonato { get; set; }

        [Column("id_jogador1")]
        public int? IdJogador1 { get; set; }
        [ForeignKey("IdJogador1")]
        public virtual Jogador? Jogador1 { get; set; }

        [Column("id_jogador2")]
        public int? IdJogador2 { get; set; }
        [ForeignKey("IdJogador2")]
        public virtual Jogador? Jogador2 { get; set; }

        [Column("nome")] public string? Nome { get; set; }
        [Column("nivel_jogador1")] public int NivelJogador1 { get; set; }
        [Column("nivel_jogador2")] public int NivelJogador2 { get; set; }
        [Column("ativo")] public bool Ativo { get; set; }
        [Column("data_criacao")] public DateTime? DataCriacao { get; set; }
    }
}