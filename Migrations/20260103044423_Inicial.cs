using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeladaControladaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_fases",
                columns: table => new
                {
                    id_fase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_fases", x => x.id_fase);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_tipos_campeonato",
                columns: table => new
                {
                    id_tipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_tipos_campeonato", x => x.id_tipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_tipos_equipe",
                columns: table => new
                {
                    id_tipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_tipos_equipe", x => x.id_tipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_cadastro = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_usuarios", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_categs_equipe",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_tipo = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pasta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_categs_equipe", x => x.id_categoria);
                    table.ForeignKey(
                        name: "FK_scc_categs_equipe_scc_tipos_equipe_id_tipo",
                        column: x => x.id_tipo,
                        principalTable: "scc_tipos_equipe",
                        principalColumn: "id_tipo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_campeonatos",
                columns: table => new
                {
                    id_campeonato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_tipo = table.Column<int>(type: "int", nullable: false),
                    id_fase = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    data_fim = table.Column<DateOnly>(type: "date", nullable: true),
                    disputa_terceiro_lugar = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    disputa_em_dupla = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ida_e_volta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    trofeu = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salvo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    criado_auto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    partida_final = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    data_criado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_salvo = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_campeonatos", x => x.id_campeonato);
                    table.ForeignKey(
                        name: "FK_scc_campeonatos_scc_fases_id_fase",
                        column: x => x.id_fase,
                        principalTable: "scc_fases",
                        principalColumn: "id_fase",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_campeonatos_scc_tipos_campeonato_id_tipo",
                        column: x => x.id_tipo,
                        principalTable: "scc_tipos_campeonato",
                        principalColumn: "id_tipo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_campeonatos_scc_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "scc_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_jogadores",
                columns: table => new
                {
                    id_jogador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario_dono = table.Column<int>(type: "int", nullable: true),
                    id_usuario_rel = table.Column<int>(type: "int", nullable: true),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    foto = table.Column<byte[]>(type: "longblob", nullable: true),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    pontuacao_anterior = table.Column<int>(type: "int", nullable: false),
                    pontuacao_atual = table.Column<int>(type: "int", nullable: false),
                    posicao_ranking_anterior = table.Column<int>(type: "int", nullable: false),
                    posicao_ranking_atual = table.Column<int>(type: "int", nullable: false),
                    qtd_campeao_consec_max = table.Column<int>(type: "int", nullable: false),
                    qtd_campeao_consec_atual = table.Column<int>(type: "int", nullable: false),
                    qtd_artilheiro_consec_max = table.Column<int>(type: "int", nullable: false),
                    qtd_artilheiro_consec_atual = table.Column<int>(type: "int", nullable: false),
                    qtd_melhordefesa_consec_max = table.Column<int>(type: "int", nullable: false),
                    qtd_melhordefesa_consec_atual = table.Column<int>(type: "int", nullable: false),
                    qtd_vitorias_consec_max = table.Column<int>(type: "int", nullable: false),
                    qtd_vitorias_consec_atual = table.Column<int>(type: "int", nullable: false),
                    mediagols_max = table.Column<double>(type: "double", nullable: false),
                    qtd_campeonatos = table.Column<int>(type: "int", nullable: false),
                    qtd_partidas = table.Column<int>(type: "int", nullable: false),
                    qtd_vitorias = table.Column<int>(type: "int", nullable: false),
                    qtd_derrotas = table.Column<int>(type: "int", nullable: false),
                    qtd_empates = table.Column<int>(type: "int", nullable: false),
                    qtd_gols = table.Column<int>(type: "int", nullable: false),
                    qtd_titulos = table.Column<int>(type: "int", nullable: false),
                    qtd_artilharias = table.Column<int>(type: "int", nullable: false),
                    qtd_defesas = table.Column<int>(type: "int", nullable: false),
                    qtd_lanternas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_jogadores", x => x.id_jogador);
                    table.ForeignKey(
                        name: "FK_scc_jogadores_scc_usuarios_id_usuario_dono",
                        column: x => x.id_usuario_dono,
                        principalTable: "scc_usuarios",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_equipes",
                columns: table => new
                {
                    id_equipe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sigla = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_equipes", x => x.id_equipe);
                    table.ForeignKey(
                        name: "FK_scc_equipes_scc_categs_equipe_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "scc_categs_equipe",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_grupos",
                columns: table => new
                {
                    id_grupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_campeonato = table.Column<int>(type: "int", nullable: false),
                    id_jogador1 = table.Column<int>(type: "int", nullable: true),
                    id_jogador2 = table.Column<int>(type: "int", nullable: true),
                    nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel_jogador1 = table.Column<int>(type: "int", nullable: false),
                    nivel_jogador2 = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_grupos", x => x.id_grupo);
                    table.ForeignKey(
                        name: "FK_scc_grupos_scc_campeonatos_id_campeonato",
                        column: x => x.id_campeonato,
                        principalTable: "scc_campeonatos",
                        principalColumn: "id_campeonato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_grupos_scc_jogadores_id_jogador1",
                        column: x => x.id_jogador1,
                        principalTable: "scc_jogadores",
                        principalColumn: "id_jogador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scc_grupos_scc_jogadores_id_jogador2",
                        column: x => x.id_jogador2,
                        principalTable: "scc_jogadores",
                        principalColumn: "id_jogador",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_pontuacoes",
                columns: table => new
                {
                    id_pontuacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_jogador = table.Column<int>(type: "int", nullable: false),
                    id_campeonato = table.Column<int>(type: "int", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_pontuacoes", x => x.id_pontuacao);
                    table.ForeignKey(
                        name: "FK_scc_pontuacoes_scc_campeonatos_id_campeonato",
                        column: x => x.id_campeonato,
                        principalTable: "scc_campeonatos",
                        principalColumn: "id_campeonato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_pontuacoes_scc_jogadores_id_jogador",
                        column: x => x.id_jogador,
                        principalTable: "scc_jogadores",
                        principalColumn: "id_jogador",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_times",
                columns: table => new
                {
                    id_time = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_campeonato = table.Column<int>(type: "int", nullable: false),
                    id_equipe = table.Column<int>(type: "int", nullable: false),
                    id_grupo = table.Column<int>(type: "int", nullable: true),
                    posicao_final = table.Column<int>(type: "int", nullable: false),
                    qtd_jogos = table.Column<int>(type: "int", nullable: false),
                    vitorias = table.Column<int>(type: "int", nullable: false),
                    empates = table.Column<int>(type: "int", nullable: false),
                    derrotas = table.Column<int>(type: "int", nullable: false),
                    golspro = table.Column<int>(type: "int", nullable: false),
                    golscontra = table.Column<int>(type: "int", nullable: false),
                    melhor_ataque = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    melhor_defesa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    media_gols = table.Column<double>(type: "double", nullable: false),
                    nivel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_times", x => x.id_time);
                    table.ForeignKey(
                        name: "FK_scc_times_scc_campeonatos_id_campeonato",
                        column: x => x.id_campeonato,
                        principalTable: "scc_campeonatos",
                        principalColumn: "id_campeonato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_times_scc_equipes_id_equipe",
                        column: x => x.id_equipe,
                        principalTable: "scc_equipes",
                        principalColumn: "id_equipe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_times_scc_grupos_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "scc_grupos",
                        principalColumn: "id_grupo");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_classificacao",
                columns: table => new
                {
                    id_classificacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_campeonato = table.Column<int>(type: "int", nullable: false),
                    id_time = table.Column<int>(type: "int", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    vitorias = table.Column<int>(type: "int", nullable: false),
                    empates = table.Column<int>(type: "int", nullable: false),
                    derrotas = table.Column<int>(type: "int", nullable: false),
                    golspro = table.Column<int>(type: "int", nullable: false),
                    golscontra = table.Column<int>(type: "int", nullable: false),
                    salgogols = table.Column<int>(type: "int", nullable: false),
                    jogos = table.Column<int>(type: "int", nullable: false),
                    grupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_classificacao", x => x.id_classificacao);
                    table.ForeignKey(
                        name: "FK_scc_classificacao_scc_campeonatos_id_campeonato",
                        column: x => x.id_campeonato,
                        principalTable: "scc_campeonatos",
                        principalColumn: "id_campeonato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_classificacao_scc_times_id_time",
                        column: x => x.id_time,
                        principalTable: "scc_times",
                        principalColumn: "id_time",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scc_jogos",
                columns: table => new
                {
                    id_jogo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_campeonato = table.Column<int>(type: "int", nullable: false),
                    id_fase = table.Column<int>(type: "int", nullable: false),
                    id_time1 = table.Column<int>(type: "int", nullable: false),
                    id_time2 = table.Column<int>(type: "int", nullable: false),
                    rodada = table.Column<int>(type: "int", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    gol_time1 = table.Column<int>(type: "int", nullable: false),
                    gol_time2 = table.Column<int>(type: "int", nullable: false),
                    penalti = table.Column<int>(type: "int", nullable: false),
                    grupo = table.Column<int>(type: "int", nullable: false),
                    salvo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    criado_auto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    partida_final = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    data_criado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_salvo = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scc_jogos", x => x.id_jogo);
                    table.ForeignKey(
                        name: "FK_scc_jogos_scc_campeonatos_id_campeonato",
                        column: x => x.id_campeonato,
                        principalTable: "scc_campeonatos",
                        principalColumn: "id_campeonato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_jogos_scc_fases_id_fase",
                        column: x => x.id_fase,
                        principalTable: "scc_fases",
                        principalColumn: "id_fase",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scc_jogos_scc_times_id_time1",
                        column: x => x.id_time1,
                        principalTable: "scc_times",
                        principalColumn: "id_time",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scc_jogos_scc_times_id_time2",
                        column: x => x.id_time2,
                        principalTable: "scc_times",
                        principalColumn: "id_time",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_scc_campeonatos_id_fase",
                table: "scc_campeonatos",
                column: "id_fase");

            migrationBuilder.CreateIndex(
                name: "IX_scc_campeonatos_id_tipo",
                table: "scc_campeonatos",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_scc_campeonatos_id_usuario",
                table: "scc_campeonatos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_scc_categs_equipe_id_tipo",
                table: "scc_categs_equipe",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_scc_classificacao_id_campeonato",
                table: "scc_classificacao",
                column: "id_campeonato");

            migrationBuilder.CreateIndex(
                name: "IX_scc_classificacao_id_time",
                table: "scc_classificacao",
                column: "id_time");

            migrationBuilder.CreateIndex(
                name: "IX_scc_equipes_id_categoria",
                table: "scc_equipes",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_scc_grupos_id_campeonato",
                table: "scc_grupos",
                column: "id_campeonato");

            migrationBuilder.CreateIndex(
                name: "IX_scc_grupos_id_jogador1",
                table: "scc_grupos",
                column: "id_jogador1");

            migrationBuilder.CreateIndex(
                name: "IX_scc_grupos_id_jogador2",
                table: "scc_grupos",
                column: "id_jogador2");

            migrationBuilder.CreateIndex(
                name: "IX_scc_jogadores_id_usuario_dono",
                table: "scc_jogadores",
                column: "id_usuario_dono");

            migrationBuilder.CreateIndex(
                name: "IX_scc_jogos_id_campeonato",
                table: "scc_jogos",
                column: "id_campeonato");

            migrationBuilder.CreateIndex(
                name: "IX_scc_jogos_id_fase",
                table: "scc_jogos",
                column: "id_fase");

            migrationBuilder.CreateIndex(
                name: "IX_scc_jogos_id_time1",
                table: "scc_jogos",
                column: "id_time1");

            migrationBuilder.CreateIndex(
                name: "IX_scc_jogos_id_time2",
                table: "scc_jogos",
                column: "id_time2");

            migrationBuilder.CreateIndex(
                name: "IX_scc_pontuacoes_id_campeonato",
                table: "scc_pontuacoes",
                column: "id_campeonato");

            migrationBuilder.CreateIndex(
                name: "IX_scc_pontuacoes_id_jogador",
                table: "scc_pontuacoes",
                column: "id_jogador");

            migrationBuilder.CreateIndex(
                name: "IX_scc_times_id_campeonato",
                table: "scc_times",
                column: "id_campeonato");

            migrationBuilder.CreateIndex(
                name: "IX_scc_times_id_equipe",
                table: "scc_times",
                column: "id_equipe");

            migrationBuilder.CreateIndex(
                name: "IX_scc_times_id_grupo",
                table: "scc_times",
                column: "id_grupo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scc_classificacao");

            migrationBuilder.DropTable(
                name: "scc_jogos");

            migrationBuilder.DropTable(
                name: "scc_pontuacoes");

            migrationBuilder.DropTable(
                name: "scc_times");

            migrationBuilder.DropTable(
                name: "scc_equipes");

            migrationBuilder.DropTable(
                name: "scc_grupos");

            migrationBuilder.DropTable(
                name: "scc_categs_equipe");

            migrationBuilder.DropTable(
                name: "scc_campeonatos");

            migrationBuilder.DropTable(
                name: "scc_jogadores");

            migrationBuilder.DropTable(
                name: "scc_tipos_equipe");

            migrationBuilder.DropTable(
                name: "scc_fases");

            migrationBuilder.DropTable(
                name: "scc_tipos_campeonato");

            migrationBuilder.DropTable(
                name: "scc_usuarios");
        }
    }
}
