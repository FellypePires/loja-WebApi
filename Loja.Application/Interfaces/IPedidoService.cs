using Loja.Application.DTOs;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<int> IniciarPedidoAsync(CriarPedidoRequest? req);
        Task AdicionarProdutoAsync(int pedidoId, int produtoId, int quantidade);
        Task AtualizarQuantidadeAsync(int pedidoId, AtualizarQuantidadeRequest req);
        Task FecharPedidoAsync(int pedidoId);
        Task<PedidoDto?> ObterPedidoAsync(int id);
    }
}
