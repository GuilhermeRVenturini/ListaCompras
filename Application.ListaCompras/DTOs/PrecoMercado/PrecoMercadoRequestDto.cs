using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.PrecoMercado
{
    public sealed class PrecoMercadoRequestDto
    {
        [Range(1, int.MaxValue)]
        public int PrecoId { get; init; }
    }
}
