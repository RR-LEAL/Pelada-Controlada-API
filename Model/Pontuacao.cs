
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_pontuacoes")]
    public class Pontuacao
    {
        [Key]
        [Column("id_pontuacao")]
        public int Id { get; set; } // Note que no diagrama o ID é o último campo (54), mas é PK

        [Column("id_jogador")]
        public int IdJogador { get; set; }
        [ForeignKey("IdJogador")]
        public virtual Jogador? Jogador { get; set; }

        [Column("id_campeonato")]
        public int IdCampeonato { get; set; }
        [ForeignKey("IdCampeonato")]
        public virtual Campeonato? Campeonato { get; set; }

        [Column("pontos")] public int Pontos { get; set; }
        [Column("data")] public DateTime Data { get; set; }
    }
}