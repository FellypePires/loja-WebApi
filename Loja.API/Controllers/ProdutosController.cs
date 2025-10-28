using Loja.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loja.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service) => _service = service;

        /// <summary>Lista todos os produtos.</summary>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.ListarAsync());

        /// <summary>Cria um novo produto.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string nome, [FromQuery] decimal preco)
        {
            var p = await _service.CriarAsync(nome, preco);
            return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _service.ObterAsync(id);
            return p is null ? NotFound() : Ok(p);
        }
    }
}