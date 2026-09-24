using Application.ListaCompras.DTOs.PrecoMercado;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PrecoMercadoController : ControllerBase
{
    private readonly IPrecoMercadoService _service;

    public PrecoMercadoController(IPrecoMercadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpGet("{produtoId:int}/{mercadoId:int}")]
    public async Task<IActionResult> GetByIdAsync(int produtoId, int mercadoId)
    {
        var result = await _service.GetByIdAsync(produtoId, mercadoId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PrecoMercadoRequestDto request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { produtoId = result.Data.ProdutoId, mercadoId = result.Data.MercadoId },
            result.Data);
    }

    [HttpPut("{produtoId:int}/{mercadoId:int}")]
    public async Task<IActionResult> UpdateAsync(int produtoId, int mercadoId, [FromBody] PrecoMercadoRequestDto request)
    {
        var result = await _service.UpdateAsync(produtoId, mercadoId, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{produtoId:int}/{mercadoId:int}")]
    public async Task<IActionResult> DeleteAsync(int produtoId, int mercadoId)
    {
        var result = await _service.DeleteAsync(produtoId, mercadoId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }
}
