using Application.ListaCompras.DTOs.Lista;
using Application.ListaCompras.DTOs.ProdutoLista;
using Application.ListaCompras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.ListaCompras.Controllers;

[ApiController]
[Route("api/listas")]
public sealed class ListaController : ControllerBase
{
    private readonly IListaService _listaService;
    private readonly IProdutoListaService _produtoListaService;
    private readonly IUsuarioListaService _usuarioListaService;

    public ListaController(
        IListaService listaService,
        IProdutoListaService produtoListaService,
        IUsuarioListaService usuarioListaService)
    {
        _listaService = listaService;
        _produtoListaService = produtoListaService;
        _usuarioListaService = usuarioListaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _listaService.GetAllAsync();

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
        var result = await _listaService.GetByIdAsync(id);

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
    public async Task<IActionResult> CreateAsync([FromBody] ListaRequestDto request)
    {
        var result = await _listaService.CreateAsync(request);

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
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ListaRequestDto request)
    {
        var result = await _listaService.UpdateAsync(id, request);

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
        var result = await _listaService.DeleteAsync(id);

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

    [HttpGet("{listaId:int}/produtos")]
    public async Task<IActionResult> GetProdutosAsync(int listaId)
    {
        var result = await _produtoListaService.GetByListaAsync(listaId);

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

    [HttpGet("{listaId:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> GetProdutoAsync(int listaId, int produtoId)
    {
        var result = await _produtoListaService.GetByIdAsync(produtoId, listaId);

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

    [HttpPost("{listaId:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> AddProdutoAsync(
        int listaId,
        int produtoId,
        [FromBody] ProdutoListaRequestDto request)
    {
        var result = await _produtoListaService.CreateAsync(listaId, produtoId, request);

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

    [HttpPut("{listaId:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> UpdateProdutoAsync(
        int listaId,
        int produtoId,
        [FromBody] ProdutoListaRequestDto request)
    {
        var result = await _produtoListaService.UpdateAsync(listaId, produtoId, request);

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

    [HttpDelete("{listaId:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> RemoveProdutoAsync(int listaId, int produtoId)
    {
        var result = await _produtoListaService.DeleteAsync(produtoId, listaId);

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

    [HttpGet("{listaId:int}/usuarios")]
    public async Task<IActionResult> GetUsuariosAsync(int listaId)
    {
        var result = await _usuarioListaService.GetByListaAsync(listaId);

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

    [HttpGet("{listaId:int}/usuarios/{usuarioId:guid}")]
    public async Task<IActionResult> GetUsuarioAsync(int listaId, Guid usuarioId)
    {
        var result = await _usuarioListaService.GetByIdAsync(listaId, usuarioId);

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

    [HttpPost("{listaId:int}/usuarios/{usuarioId:guid}")]
    public async Task<IActionResult> AddUsuarioAsync(int listaId, Guid usuarioId)
    {
        var result = await _usuarioListaService.CreateAsync(listaId, usuarioId);

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

    [HttpDelete("{listaId:int}/usuarios/{usuarioId:guid}")]
    public async Task<IActionResult> RemoveUsuarioAsync(int listaId, Guid usuarioId)
    {
        var result = await _usuarioListaService.DeleteAsync(listaId, usuarioId);

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

}
