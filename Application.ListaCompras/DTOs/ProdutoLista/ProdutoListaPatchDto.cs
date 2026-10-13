using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.ProdutoLista;

public sealed class ProdutoListaPatchDto
{
    [Range(1, int.MaxValue)]
    public int? StatusId { get; init; }

    [Range(0, int.MaxValue)]
    public int? QuantidadeEstoque { get; init; }

}
