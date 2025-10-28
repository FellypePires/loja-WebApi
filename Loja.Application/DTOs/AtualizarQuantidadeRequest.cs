namespace Loja.Application.DTOs;

public class AtualizarQuantidadeRequest
{
    public int ProdutoId { get; set; }
    // se for 0 ou menor, o item será removido
    public int Quantidade { get; set; }
}
