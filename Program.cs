using Microsoft.EntityFrameworkCore;
using PeladaControladaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// ==========================================
// 2. CONFIGURAÇÃO DO PIPELINE (REQUESTS)
// ==========================================

// Habilita o Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Isso cria a tela visual em /swagger
}

app.UseHttpsRedirection();

// Mapeia os Controllers (seus futuros arquivos de rota)
app.MapControllers(); 

app.Run();