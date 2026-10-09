using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Produto;

public sealed class ProdutoPatchDto
{
    [MaxLength(128)]
    public string? Nome { get; init; }

    [MaxLength(1024)]
    public string? Descricao { get; init; }

    [MaxLength(128)]
    public string? Fabricante { get; init; }

    public DateOnly? Validade { get; init; }

}
