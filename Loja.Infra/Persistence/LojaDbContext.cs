using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infra.Persistence;

public class LojaDbContext : DbContext
{
    public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produtos");
            e.Property(p => p.Nome).HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");
            e.HasMany(p => p.Itens)
             .WithOne()
             .HasForeignKey(i => i.PedidoId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PedidoItem>(e =>
        {
            e.ToTable("PedidoItens");
            e.Property(i => i.ProdutoNome).HasMaxLength(120).IsRequired();
        });
    }
}
