using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeladaControladaAPI.model
{
    [Table("scc_jogadores")]
    public class Jogador
    {
        [Key]
        [Column("id_jogador")]
        public int Id { get; set; }

        [Column("id_usuario_dono")]
        public int? IdUsuarioDono { get; set; }
        [ForeignKey("IdUsuarioDono")]
        public virtual Usuario? UsuarioDono { get; set; }

        [Column("id_usuario_rel")]
        public int? IdUsuarioRel { get; set; } 

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("foto")]
        public byte[]? Foto { get; set; } // BLOB

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("data_criacao")]
        public DateTime? DataCriacao { get; set; }

        // Estatísticas (Mapeamento completo do diagrama)
        [Column("pontuacao_anterior")] public int PontuacaoAnterior { get; set; }
        [Column("pontuacao_atual")] public int PontuacaoAtual { get; set; }
        [Column("posicao_ranking_anterior")] public int PosicaoRankingAnterior { get; set; }
        [Column("posicao_ranking_atual")] public int PosicaoRankingAtual { get; set; }
        [Column("qtd_campeao_consec_max")] public int QtdCampeaoConsecMax { get; set; }
        [Column("qtd_campeao_consec_atual")] public int QtdCampeaoConsecAtual { get; set; }
        [Column("qtd_artilheiro_consec_max")] public int QtdArtilheiroConsecMax { get; set; }
        [Column("qtd_artilheiro_consec_atual")] public int QtdArtilheiroConsecAtual { get; set; }
        [Column("qtd_melhordefesa_consec_max")] public int QtdMelhorDefesaConsecMax { get; set; }
        [Column("qtd_melhordefesa_consec_atual")] public int QtdMelhorDefesaConsecAtual { get; set; }
        [Column("qtd_vitorias_consec_max")] public int QtdVitoriasConsecMax { get; set; }
        [Column("qtd_vitorias_consec_atual")] public int QtdVitoriasConsecAtual { get; set; }
        [Column("mediagols_max")] public double MediaGolsMax { get; set; }
        [Column("qtd_campeonatos")] public int QtdCampeonatos { get; set; }
        [Column("qtd_partidas")] public int QtdPartidas { get; set; }
        [Column("qtd_vitorias")] public int QtdVitorias { get; set; }
        [Column("qtd_derrotas")] public int QtdDerrotas { get; set; }
        [Column("qtd_empates")] public int QtdEmpates { get; set; }
        [Column("qtd_gols")] public int QtdGols { get; set; }
        [Column("qtd_titulos")] public int QtdTitulos { get; set; }
        [Column("qtd_artilharias")] public int QtdArtilharias { get; set; }
        [Column("qtd_defesas")] public int QtdDefesas { get; set; }
        [Column("qtd_lanternas")] public int QtdLanternas { get; set; }
    }
}