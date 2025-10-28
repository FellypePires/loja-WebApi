namespace Loja.Application.DTOs;

public class CriarPedidoRequest
{
    // Itens do novo pedido 
    public List<ItemPedidoRequest> Itens { get; set; } = new();
}
