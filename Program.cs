using Microsoft.EntityFrameworkCore;
using PeladaControladaAPI.Data;
using PeladaControladaAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 0. CONFIGURAÇÃO DO SERILOG (LOGGING)
// ==========================================
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration)
);

// ==========================================
// 1. CONFIGURAÇÃO DE SERVIÇOS (CONTAINER)
// ==========================================

// Adiciona suporte a Controllers (necessário para sua API crescer)
builder.Services.AddControllers();

// Adiciona o Swagger (Documentação Visual)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CONFIGURAÇÃO DO BANCO DE DADOS ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, 
        ServerVersion.AutoDetect(connectionString))
);

// Injeta o serviço de mensagem (pode trocar por TwilioService no futuro)
builder.Services.AddScoped<IMensagemService, EmailMensagemService>();

var app = builder.Build();

// ==========================================
// 2. CONFIGURAÇÃO DO PIPELINE (REQUESTS)
// ==========================================

// Adiciona middleware de logging HTTP
app.UseSerilogRequestLogging();

// Habilita o Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Isso cria a tela visual em /swagger
}

app.UseHttpsRedirection();

// Mapeia os Controllers (seus futuros arquivos de rota)
app.MapControllers();

// Log de inicialização
Log.Information("🚀 Aplicação iniciada com sucesso!");

app.Run();