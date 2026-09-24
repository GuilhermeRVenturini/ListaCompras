using Application.ListaCompras.DTOs.ProdutoLista;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProdutoListaController : ControllerBase
{
    private readonly IProdutoListaService _service;

    public ProdutoListaController(IProdutoListaService service)
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

    [HttpGet("{produtoId:int}/{listaId:int}")]
    public async Task<IActionResult> GetByIdAsync(int produtoId, int listaId)
    {
        var result = await _service.GetByIdAsync(produtoId, listaId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProdutoListaRequestDto request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { produtoId = result.Data.ProdutoId, listaId = result.Data.ListaId },
            result.Data);
    }

    [HttpPut("{produtoId:int}/{listaId:int}")]
    public async Task<IActionResult> UpdateAsync(int produtoId, int listaId, [FromBody] ProdutoListaRequestDto request)
    {
        var result = await _service.UpdateAsync(produtoId, listaId, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{produtoId:int}/{listaId:int}")]
    public async Task<IActionResult> DeleteAsync(int produtoId, int listaId)
    {
        var result = await _service.DeleteAsync(produtoId, listaId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }
}
