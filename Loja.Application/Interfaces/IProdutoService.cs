using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> CriarAsync(string nome, decimal preco);
        Task<ProdutoDto?> ObterAsync(int id);
        Task<List<ProdutoDto>> ListarAsync();
    }
}