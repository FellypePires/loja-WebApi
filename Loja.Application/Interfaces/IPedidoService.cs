using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IPedidoService
    {
        // Cria um novo pedido
        Task<int> IniciarPedidoAsync(CriarPedidoRequest? req);

        // Adiciona produto a um pedido existente
        Task AdicionarProdutoAsync(int pedidoId, int produtoId, int quantidade);

        // Atualiza quantidade de um item no pedido
        Task AtualizarQuantidadeAsync(int pedidoId, AtualizarQuantidadeRequest req);

        // Fecha um pedido (não pode mais ser alterado)
        Task FecharPedidoAsync(int pedidoId);

        // Retorna um pedido detalhado
        Task<PedidoDto?> ObterPedidoAsync(int id);

        // Relatório de saída de produtos
        Task<IEnumerable<RelatorioProdutoDto>> GerarRelatorioSaidaProdutosAsync();
    }
}
