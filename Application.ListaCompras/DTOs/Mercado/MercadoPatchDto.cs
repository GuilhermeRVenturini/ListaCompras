using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Mercado;

public sealed class MercadoPatchDto
{
    [MaxLength(64)]
    public string? Nome { get; init; }

    [MaxLength(256)]
    public string? HorarioFuncionamento { get; init; }

    [MaxLength(256)]
    public string? Endereco { get; init; }

}
