using Application.ListaCompras.DTOs.PrecoMercado;
using Application.ListaCompras.DTOs.Produto;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPut("{id:int}")]
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

    [HttpGet("{produtoId:int}/mercados/{mercadoId:int}/preco")]
    public async Task<IActionResult> GetPrecoMercadoAsync(int produtoId, int mercadoId)
    {
        var result = await _precoMercadoService.GetByIdAsync(produtoId, mercadoId);

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

    [HttpPost("{produtoId:int}/mercados/{mercadoId:int}/preco")]
    public async Task<IActionResult> AddPrecoMercadoAsync(
        int produtoId,
        int mercadoId,
        [FromBody] PrecoMercadoRequestDto request)
    {
        var result = await _precoMercadoService.CreateAsync(produtoId, mercadoId, request);

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

    [HttpPut("{produtoId:int}/mercados/{mercadoId:int}/preco")]
    public async Task<IActionResult> UpdatePrecoMercadoAsync(
        int produtoId,
        int mercadoId,
        [FromBody] PrecoMercadoRequestDto request)
    {
        var result = await _precoMercadoService.UpdateAsync(produtoId, mercadoId, request);

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

    [HttpDelete("{produtoId:int}/mercados/{mercadoId:int}/preco")]
    public async Task<IActionResult> RemovePrecoMercadoAsync(int produtoId, int mercadoId)
    {
        var result = await _precoMercadoService.DeleteAsync(produtoId, mercadoId);

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

}
