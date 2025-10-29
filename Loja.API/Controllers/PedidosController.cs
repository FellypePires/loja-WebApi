using Loja.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Loja.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
        => _pedidoService = pedidoService;

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest req)
    {
        var id = await _pedidoService.IniciarPedidoAsync(req);
        var pedido = await _pedidoService.ObterPedidoAsync(id);
        return CreatedAtAction(nameof(Obter), new { id }, pedido);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obter(int id)
    {
        var pedido = await _pedidoService.ObterPedidoAsync(id);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpPost("{id:int}/itens")]
    public async Task<IActionResult> AdicionarItem(int id, [FromBody] ItemPedidoRequest req)
    {
        await _pedidoService.AdicionarProdutoAsync(id, req.ProdutoId, req.Quantidade);
        var pedido = await _pedidoService.ObterPedidoAsync(id);
        return Ok(pedido);
    }

    [HttpPatch("{id:int}/itens")]
    public async Task<IActionResult> AtualizarQuantidade(int id, [FromBody] AtualizarQuantidadeRequest request)
    {
        await _pedidoService.AtualizarQuantidadeAsync(id, request);
        return NoContent();
    }

    [HttpPost("{id:int}/fechar")]
    public async Task<IActionResult> Fechar(int id)
    {
        await _pedidoService.FecharPedidoAsync(id);
        var pedido = await _pedidoService.ObterPedidoAsync(id);
        return Ok(pedido);
    }
    [HttpGet("relatorio/saida-produtos")]
    public async Task<IActionResult> RelatorioSaidaProdutos()
    {
        var relatorio = await _pedidoService.GerarRelatorioSaidaProdutosAsync();
        return Ok(relatorio);
    }
}

