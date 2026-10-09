using Application.ListaCompras.DTOs.Mercado;
using Application.ListaCompras.DTOs.PrecoMercado;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/mercados")]
public sealed class MercadoController : ControllerBase
{
    private readonly IMercadoService _mercadoService;
    private readonly IPrecoMercadoService _precoMercadoService;

    public MercadoController(
        IMercadoService mercadoService,
        IPrecoMercadoService precoMercadoService)
    {
        _mercadoService = mercadoService;
        _precoMercadoService = precoMercadoService;
    }

    [HttpGet]

    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _mercadoService.GetAllAsync();

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
        var result = await _mercadoService.GetByIdAsync(id);

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

    public async Task<IActionResult> CreateAsync([FromBody] MercadoRequestDto request)
    {
        var result = await _mercadoService.CreateAsync(request);

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
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] MercadoPatchDto request)
    {
        var result = await _mercadoService.UpdateAsync(id, request);

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
        var result = await _mercadoService.DeleteAsync(id);

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

    [HttpGet("{mercadoId:int}/produtos")]
    public async Task<IActionResult> GetProdutosAsync(int mercadoId)
    {
        var result = await _precoMercadoService.GetByMercadoAsync(mercadoId);

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

    [HttpGet("{mercadoId:int}/produtos/{produtoId:int}/preco")]
    public async Task<IActionResult> GetPrecoProdutoAsync(int mercadoId, int produtoId)
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

    [HttpPost("{mercadoId:int}/produtos/{produtoId:int}/preco")]
    public async Task<IActionResult> AddPrecoProdutoAsync(
        int mercadoId,
        int produtoId,
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

    /// <summary>
    /// Substitui a referência de preço de um produto em determinado mercado.
    /// O corpo informa um <c>PrecoId</c> existente; a operação não altera
    /// diretamente os valores do registro de preço.
    /// </summary>
    [HttpPatch("{mercadoId:int}/produtos/{produtoId:int}/preco")]
    public async Task<IActionResult> UpdatePrecoProdutoAsync(
        int mercadoId,
        int produtoId,
        [FromBody] PrecoMercadoPatchDto request)
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

    [HttpDelete("{mercadoId:int}/produtos/{produtoId:int}/preco")]
    public async Task<IActionResult> RemovePrecoProdutoAsync(int mercadoId, int produtoId)
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
