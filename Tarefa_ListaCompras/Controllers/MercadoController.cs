using Application.ListaCompras.DTOs.Mercado;
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] MercadoRequestDto request)
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

}
