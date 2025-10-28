using Loja.Domain.Enums;

namespace Loja.Domain.Entities;

public class Pedido
{
    public int Id { get; private set; }
    public PedidoStatus Status { get; private set; } = PedidoStatus.Aberto;

    public ICollection<PedidoItem> Itens { get; private set; } = new List<PedidoItem>();

    public decimal Total => Itens.Sum(i => i.Subtotal);

    public Pedido() { }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        if (Status == PedidoStatus.Fechado)
            throw new InvalidOperationException("Não é possível alterar um pedido fechado.");

        var existente = Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
        if (existente is null)
            Itens.Add(new PedidoItem(produto.Id, produto.Nome, produto.Preco, quantidade));
        else
            existente.AtualizarQuantidade(existente.Quantidade + quantidade);
    }

    public void AtualizarQuantidade(int produtoId, int quantidade)
    {
        if (Status == PedidoStatus.Fechado)
            throw new InvalidOperationException("Não é possível alterar um pedido fechado.");

        var item = Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item is null) return;

        if (quantidade <= 0)
            Itens.Remove(item);
        else
            item.AtualizarQuantidade(quantidade);
    }

    public void Fechar()
    {
        if (!Itens.Any())
            throw new InvalidOperationException("Não é possível fechar um pedido sem itens.");

        Status = PedidoStatus.Fechado;
    }
}
