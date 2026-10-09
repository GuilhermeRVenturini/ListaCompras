using Application.ListaCompras.DTOs.Preco;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

// controller de Preco: recebe a requisição e manda pro service resolver
namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/precos")]
public sealed class PrecoController : ControllerBase
{
    private readonly IPrecoService _service;

    // endpoint: recebe os parâmetros, chama o service e devolve a resposta HTTP
    public PrecoController(IPrecoService service)
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

    [HttpPost]
    // cria um registro novo e devolve o resultado
    public async Task<IActionResult> CreateAsync([FromBody] PrecoRequestDto request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpPatch("{id:int}")]
    // acha o registro e atualiza os dados dele
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PrecoRequestDto request)
    {
        var result = await _service.UpdateAsync(id, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    // remove o registro usando o id
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }
}
