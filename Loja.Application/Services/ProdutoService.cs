using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Loja.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly LojaDbContext _db;

        public ProdutoService(LojaDbContext db) => _db = db;

        public async Task<ProdutoDto> CriarAsync(string nome, decimal preco)
        {
            var prod = new Produto(nome, preco);
            _db.Produtos.Add(prod);
            await _db.SaveChangesAsync();

            return new ProdutoDto { Id = prod.Id, Nome = prod.Nome, Preco = prod.Preco };
        }

        public async Task<ProdutoDto?> ObterAsync(int id)
        {
            var p = await _db.Produtos.FindAsync(id);
            return p is null ? null : new ProdutoDto { Id = p.Id, Nome = p.Nome, Preco = p.Preco };
        }

        public async Task<List<ProdutoDto>> ListarAsync()
        {
            return await _db.Produtos
                .Select(p => new ProdutoDto { Id = p.Id, Nome = p.Nome, Preco = p.Preco })
                .ToListAsync();
        }
    }
}