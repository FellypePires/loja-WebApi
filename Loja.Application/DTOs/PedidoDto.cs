namespace Loja.Application.DTOs;

public class PedidoItemDto
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = "";
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }
}

public class PedidoDto
{
    public int Id { get; set; }
    public string Status { get; set; } = "";
    public decimal Total { get; set; }
    public List<PedidoItemDto> Itens { get; set; } = new();
}
