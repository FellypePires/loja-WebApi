using Loja.Application.Interfaces;
using Loja.Application.Services;
using Loja.Domain.Entities;
using Loja.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//  CONFIGURA��O DO BANCO DE DADOS (SQLite)

builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LojaDb")));

//  INJE��O DE DEPEND�NCIAS (SERVI�OS)

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Loja WebAPI",
        Version = "v1",
        Description = "API para gerenciamento de pedidos e produtos.",
        Contact = new OpenApiContact
        {
            Name = "Fellype Gabriel",
            Email = "fellype.pires@insidesistemas.com.br",
            Url = new Uri("https://github.com/FellypePires")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<LojaDbContext>();

    // Aplica as migrations automaticamente, caso n�o existam
    ctx.Database.Migrate();

    if (!ctx.Produtos.Any())
    {
        ctx.Produtos.AddRange(
            new Produto { Nome = "Camiseta Premium", Preco = 89.90m },
            new Produto { Nome = "Cal�a Jeans", Preco = 199.90m },
            new Produto { Nome = "T�nis Esportivo", Preco = 349.90m },
            new Produto { Nome = "Jaqueta de Couro", Preco = 499.90m },
            new Produto { Nome = "Rel�gio Smart", Preco = 799.90m },
            new Produto { Nome = "Bon�", Preco = 69.90m },
            new Produto { Nome = "Moletom", Preco = 229.90m },
            new Produto { Nome = "Camisa Social", Preco = 139.90m },
            new Produto { Nome = "Cinto de Couro", Preco = 99.90m },
            new Produto { Nome = "Carteira", Preco = 59.90m }
        );

        ctx.SaveChanges();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Produtos iniciais adicionados com sucesso!");
        Console.ResetColor();
    }
}


//  CONFIGURA��O DO PIPELINE E MIDDLEWARE

// Ativa o CORS
app.UseCors("AllowAll");

// Swagger (somente em desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loja WebAPI v1");
        c.RoutePrefix = string.Empty; 
    });
}

// HTTPS e Controllers
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
