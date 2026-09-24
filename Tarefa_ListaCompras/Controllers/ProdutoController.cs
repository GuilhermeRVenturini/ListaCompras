using Application.ListaCompras.DTOs.Produto;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProdutoRequestDto request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = result.Data.Id },
            result.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProdutoRequestDto request)
    {
        var result = await _service.UpdateAsync(id, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }
}
