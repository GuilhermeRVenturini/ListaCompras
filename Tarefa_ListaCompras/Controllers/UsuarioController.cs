using Application.ListaCompras.DTOs.Usuario;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

// controller de Usuario: recebe a requisição e manda pro service resolver
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
    // busca a lista toda; no usuário dá pra passar o nome pra filtrar
    public async Task<IActionResult> GetAllAsync([FromQuery] string? nome)
    {
        var result = await _usuarioService.GetAllAsync(nome);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    // procura um registro pelo id, se não achar volta erro
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _usuarioService.GetByIdAsync(id);

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
    public async Task<IActionResult> CreateAsync([FromBody] UsuarioRequestDto request)
    {
        var result = await _usuarioService.CreateAsync(request);

        if (!result.IsSuccess || result.Data is null)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpPatch("{id:guid}")]
    // acha o registro e atualiza os dados dele
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UsuarioRequestDto request)
    {
        var result = await _usuarioService.UpdateAsync(id, request);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    // remove o registro usando o id
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _usuarioService.DeleteAsync(id);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return NoContent();
    }

    [HttpGet("{usuarioId:guid}/listas")]
    // endpoint: recebe os parâmetros, chama o service e devolve a resposta HTTP
    public async Task<IActionResult> GetListasAsync(Guid usuarioId)
    {
        var result = await _usuarioListaService.GetByUsuarioAsync(usuarioId);

        if (!result.IsSuccess || result.Data is null)
        {
            if (result.Message.Contains("não encontrad", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { result.Message });

            return BadRequest(new { result.Message });
        }

        return Ok(result.Data);
    }
}
