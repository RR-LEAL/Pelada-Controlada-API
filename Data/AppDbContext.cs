using Microsoft.EntityFrameworkCore;
using PeladaControladaAPI.model;
using PeladaControladaAPI.Model;

namespace PeladaControladaAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor obrigatório para injeção de dependência
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ========================================================================
        // REGISTRO DAS TABELAS (DbSets)
        // ========================================================================

        // Tabelas Básicas / Cadastros
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoCampeonato> TiposCampeonato { get; set; }
        public DbSet<Fase> Fases { get; set; }
        public DbSet<TipoEquipe> TiposEquipe { get; set; }
        public DbSet<CategoriaEquipe> CategoriasEquipe { get; set; }
        public DbSet<Equipe> Equipes { get; set; }

        // Tabelas Principais
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Time> Times { get; set; }

        // Tabelas de Movimentação / Jogos
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Pontuacao> Pontuacoes { get; set; }
        public DbSet<Classificacao> Classificacoes { get; set; }
        public DbSet<UsuarioCodigoOtp> UsuarioCodigosOtp { get; set; }

        // ========================================================================
        // CONFIGURAÇÕES AVANÇADAS (Fluent API)
        // ========================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------------------------------------------------------
            // Configuração Especial: Tabela de Jogos (Relacionamento Duplo)
            // --------------------------------------------------------------------
            // Como um Jogo tem DOIS times (Time1 e Time2), o EF Core pode se confundir 
            // ou criar ciclos de delete em cascata. Vamos travar isso aqui.

            modelBuilder.Entity<Jogo>()
                .HasOne(j => j.Time1)
                .WithMany() // O Time não precisa ter uma lista de "JogosOndeFuiTime1" explícita
                .HasForeignKey(j => j.IdTime1)
                .OnDelete(DeleteBehavior.Restrict); // Impede que deletar um time apague os jogos automaticamente

            modelBuilder.Entity<Jogo>()
                .HasOne(j => j.Time2)
                .WithMany()
                .HasForeignKey(j => j.IdTime2)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------------------------------------------------
            // Configuração Especial: Grupos (Relacionamento Duplo com Jogadores)
            // --------------------------------------------------------------------
            // Mesma lógica: Um grupo tem Jogador1 e Jogador2.

            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.Jogador1)
                .WithMany()
                .HasForeignKey(g => g.IdJogador1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grupo>()
                .HasOne(g => g.Jogador2)
                .WithMany()
                .HasForeignKey(g => g.IdJogador2)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------------------------------------------------
            // Configuração Opcional: Tipos de Dados Específicos do MySQL
            // --------------------------------------------------------------------
            // Se quiser garantir que o BLOB da foto seja criado corretamente (longblob)
            modelBuilder.Entity<Jogador>()
                .Property(j => j.Foto)
                .HasColumnType("longblob"); // Permite arquivos maiores de imagem
        }
    }
}