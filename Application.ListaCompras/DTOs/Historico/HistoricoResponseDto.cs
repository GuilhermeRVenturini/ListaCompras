namespace Application.ListaCompras.DTOs.Historico
{
    public sealed class HistoricoResponseDto
    {
        public int Id { get; init; }
        public string Registro { get; init; } = default!;
        public int ProdutoId { get; init; }
        public int MercadoId { get; init; }
        public int PrecoId { get; init; }
        public DateTime DataRegistro { get; init; }
    }
}
