namespace Loja.Domain.Entities;

public class PedidoItem
{
    public int Id { get; private set; }
    public int PedidoId { get; private set; }
    public int ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; } = "";
    public decimal PrecoUnitario { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Subtotal => PrecoUnitario * Quantidade;

    // EF
    private PedidoItem() { }

    public PedidoItem(int produtoId, string produtoNome, decimal precoUnitario, int quantidade)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        PrecoUnitario = precoUnitario;
        Quantidade = quantidade;
    }

    public void AtualizarQuantidade(int quantidade)
    {
        Quantidade = quantidade;
    }
}
