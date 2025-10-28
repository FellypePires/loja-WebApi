using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Loja.Application.Services;

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

        // Adiciona itens ao pedido (caso existam na requisição)
        if (req?.Itens != null && req.Itens.Any())
        {
            foreach (var item in req.Itens)
            {
                var produto = await _ctx.Produtos
                    .FirstOrDefaultAsync(p => p.Id == item.ProdutoId)
                    ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

                pedido.AdicionarItem(produto, item.Quantidade);
            }
        }

        _ctx.Pedidos.Add(pedido);
        await _ctx.SaveChangesAsync();

        // Retorna o ID gerado
        return pedido.Id;
    }

 
    public async Task AdicionarProdutoAsync(int pedidoId, int produtoId, int quantidade)
    {
        var pedido = await _ctx.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == pedidoId)
            ?? throw new InvalidOperationException("Pedido não encontrado.");

        var produto = await _ctx.Produtos
            .FirstOrDefaultAsync(p => p.Id == produtoId)
            ?? throw new InvalidOperationException("Produto não encontrado.");

        pedido.AdicionarItem(produto, quantidade);

        await _ctx.SaveChangesAsync();
    }

    //  ATUALIZAR QUANTIDADE DE UM ITEM DO PEDIDO
    
    public async Task AtualizarQuantidadeAsync(int pedidoId, AtualizarQuantidadeRequest req)
    {
        var pedido = await _ctx.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == pedidoId)
            ?? throw new InvalidOperationException("Pedido não encontrado.");

        pedido.AtualizarQuantidade(req.ProdutoId, req.Quantidade);

        await _ctx.SaveChangesAsync();
    }

    
    //  FECHAR PEDIDO (não permite alterações posteriores)
    
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

        if (pedido is null)
            return null;

        return new PedidoDto
        {
            Id = pedido.Id,
            Status = pedido.Status.ToString(),
            Total = pedido.Total,
            Itens = pedido.Itens
                .Select(i => new PedidoItemDto
                {
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.ProdutoNome,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade,
                    Subtotal = i.Subtotal
                })
                .ToList()
        };
    }
}
