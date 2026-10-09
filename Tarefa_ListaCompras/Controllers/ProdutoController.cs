using Application.ListaCompras.DTOs.Produto;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

// controller de Produto: recebe a requisição e manda pro service resolver
namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/produtos")]
public sealed class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly IPrecoMercadoService _precoMercadoService;
    private readonly IProdutoListaService _produtoListaService;

    public ProdutoController(
        IProdutoService produtoService,
        IPrecoMercadoService precoMercadoService,
        IProdutoListaService produtoListaService)
    {
        _produtoService = produtoService;
        _precoMercadoService = precoMercadoService;
        _produtoListaService = produtoListaService;
    }

    [HttpGet]
    // busca a lista toda; no usuário dá pra passar o nome pra filtrar
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _produtoService.GetAllAsync();

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    // procura um registro pelo id, se não achar volta erro
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _produtoService.GetByIdAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPost]
    // cria um registro novo e devolve o resultado
    public async Task<IActionResult> CreateAsync([FromBody] ProdutoRequestDto request)
    {
        var result = await _produtoService.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPatch("{id:int}")]
    // acha o registro e atualiza os dados dele
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProdutoRequestDto request)
    {
        var result = await _produtoService.UpdateAsync(id, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    // remove o registro usando o id
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _produtoService.DeleteAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }

    [HttpGet("{produtoId:int}/listas")]
    // endpoint: recebe os parâmetros, chama o service e devolve a resposta HTTP
    public async Task<IActionResult> GetListasAsync(int produtoId)
    {
        var result = await _produtoListaService.GetByProdutoAsync(produtoId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpGet("{produtoId:int}/precos")]
    // endpoint: recebe os parâmetros, chama o service e devolve a resposta HTTP
    public async Task<IActionResult> GetPrecosAsync(int produtoId)
    {
        var result = await _precoMercadoService.GetByProdutoAsync(produtoId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            if (result.Message.Contains("já", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }
}
