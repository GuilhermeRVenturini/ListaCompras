using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Lista;

public sealed class ListaPatchDto
{
    public DateTime? DataListaCriacao { get; init; }

    [MaxLength(64)]
    public string? Nome { get; init; }

}
