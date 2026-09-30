using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/historicos")]
public sealed class HistoricoController : ControllerBase
{
    private readonly IHistoricoService _service;

    public HistoricoController(IHistoricoService service)
    {
        _service = service;
    }

    // Retorna todos os registros, do mais recente para o mais antigo.
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();

        if (!result.IsSuccess)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    // Busca um registro específico do histórico.
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.IsSuccess || result.Data is null)
            return NotFound(new { result.Message });

        return Ok(result.Data);
    }
}
