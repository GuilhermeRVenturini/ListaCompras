using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Historico
{
    public sealed class HistoricoRequestDto
    {
        [MaxLength(1024)]
        public string Registro { get; init; } = default!;

        public int ProdutoId { get; init; }
        public int MercadoId { get; init; }
        public int PrecoId { get; init; }
        public DateTime DataRegistro { get; init; }
    }
}
