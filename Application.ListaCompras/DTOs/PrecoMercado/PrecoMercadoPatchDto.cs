using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.PrecoMercado;

public sealed class PrecoMercadoPatchDto
{
    [Range(1, int.MaxValue)]
    public int? PrecoId { get; init; }

}
