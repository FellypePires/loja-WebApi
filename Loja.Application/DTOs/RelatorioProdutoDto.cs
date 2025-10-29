namespace Loja.Application.DTOs;

public class RelatorioProdutoDto
{
    public string ProdutoNome { get; set; } = string.Empty;
    public int QuantidadeVendida { get; set; }
    public decimal ValorTotalVendido { get; set; }
}
