using Application.ListaCompras.DTOs.Usuario;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/usuarios")]
public sealed class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioListaService _usuarioListaService;

    public UsuarioController(
        IUsuarioService usuarioService,
        IUsuarioListaService usuarioListaService)
    {
        _usuarioService = usuarioService;
        _usuarioListaService = usuarioListaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _usuarioService.GetAllAsync();

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _usuarioService.GetByIdAsync(id);

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
    public async Task<IActionResult> CreateAsync([FromBody] UsuarioRequestDto request)
    {
        var result = await _usuarioService.CreateAsync(request);

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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UsuarioRequestDto request)
    {
        var result = await _usuarioService.UpdateAsync(id, request);

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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _usuarioService.DeleteAsync(id);

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

    [HttpGet("{usuarioId:guid}/listas")]
    public async Task<IActionResult> GetListasAsync(Guid usuarioId)
    {
        var result = await _usuarioListaService.GetByUsuarioAsync(usuarioId);

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
