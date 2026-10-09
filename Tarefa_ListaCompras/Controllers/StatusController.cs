using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

// controller de Status: recebe a requisição e manda pro service resolver
namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/status")]
public sealed class StatusController : ControllerBase
{
    private readonly IStatusService _service;

    // endpoint: recebe os parâmetros, chama o service e devolve a resposta HTTP
    public StatusController(IStatusService service)
    {
        _service = service;
    }

    [HttpGet]
    // busca a lista toda; no usuário dá pra passar o nome pra filtrar
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    // procura um registro pelo id, se não achar volta erro
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }
}
