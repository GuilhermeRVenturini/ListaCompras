using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Preco;

public sealed class PrecoPatchDto
{
    [Range(typeof(decimal), "0", "99999999.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? Valor { get; init; }

    public bool? Desconto { get; init; }

}
