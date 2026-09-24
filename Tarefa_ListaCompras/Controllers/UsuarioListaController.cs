using Application.ListaCompras.DTOs.UsuarioLista;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsuarioListaController : ControllerBase
{
    private readonly IUsuarioListaService _service;

    public UsuarioListaController(IUsuarioListaService service)
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

    [HttpGet("{listaId:int}/{usuarioId:guid}")]
    public async Task<IActionResult> GetByIdAsync(int listaId, Guid usuarioId)
    {
        var result = await _service.GetByIdAsync(listaId, usuarioId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UsuarioListaRequestDto request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { listaId = result.Data.ListaId, usuarioId = result.Data.UsuarioId },
            result.Data);
    }

    [HttpDelete("{listaId:int}/{usuarioId:guid}")]
    public async Task<IActionResult> DeleteAsync(int listaId, Guid usuarioId)
    {
        var result = await _service.DeleteAsync(listaId, usuarioId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Equals("Registro não encontrado.", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }
}
