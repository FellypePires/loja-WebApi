namespace Loja.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        // Construtor vazio necessário para o EF Core
        public Produto() { }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }
}
