using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Loja.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly LojaDbContext _ctx;

        public PedidoService(LojaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> IniciarPedidoAsync(CriarPedidoRequest? req)
        {
            var pedido = new Pedido();

            if (req?.Itens is { Count: > 0 })
            {
                foreach (var item in req.Itens)
                {
                    var produto = await _ctx.Produtos.FirstOrDefaultAsync(p => p.Id == item.ProdutoId)
                        ?? throw new InvalidOperationException("Produto não encontrado.");

                    pedido.AdicionarItem(produto, item.Quantidade);
                }
            }

            _ctx.Pedidos.Add(pedido);
            await _ctx.SaveChangesAsync();

            return pedido.Id;
        }

        
        public async Task AdicionarProdutoAsync(int pedidoId, int produtoId, int quantidade)
        {
            var pedido = await _ctx.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId)
                ?? throw new InvalidOperationException("Pedido não encontrado.");

            var produto = await _ctx.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId)
                ?? throw new InvalidOperationException("Produto não encontrado.");

            pedido.AdicionarItem(produto, quantidade);
            await _ctx.SaveChangesAsync();
        }

      
        public async Task AtualizarQuantidadeAsync(int pedidoId, AtualizarQuantidadeRequest req)
        {
            var pedido = await _ctx.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId)
                ?? throw new InvalidOperationException("Pedido não encontrado.");

            pedido.AtualizarQuantidade(req.ProdutoId, req.Quantidade);
            await _ctx.SaveChangesAsync();
        }

        public async Task FecharPedidoAsync(int pedidoId)
        {
            var pedido = await _ctx.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId)
                ?? throw new InvalidOperationException("Pedido não encontrado.");

            pedido.Fechar();
            await _ctx.SaveChangesAsync();
        }

      
        public async Task<PedidoDto?> ObterPedidoAsync(int id)
        {
            var pedido = await _ctx.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return null;

            return new PedidoDto
            {
                Id = pedido.Id,
                Status = pedido.Status.ToString(),
                Total = pedido.Total,
                Itens = pedido.Itens.Select(i => new PedidoItemDto
                {
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.ProdutoNome,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }

        // Relatório de saída de produtos
        public async Task<IEnumerable<RelatorioProdutoDto>> GerarRelatorioSaidaProdutosAsync()
        {
            
            var itens = await _ctx.PedidoItens
                .AsNoTracking()
                .ToListAsync();
           
            var relatorio = itens
                .GroupBy(i => i.ProdutoNome ?? "Produto Desconhecido")
                .Select(g => new RelatorioProdutoDto
                {
                    ProdutoNome = g.Key,
                    QuantidadeVendida = g.Sum(x => x.Quantidade),
                    ValorTotalVendido = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(r => r.QuantidadeVendida)
                .ToList();

            return relatorio;
        }
    }
}
