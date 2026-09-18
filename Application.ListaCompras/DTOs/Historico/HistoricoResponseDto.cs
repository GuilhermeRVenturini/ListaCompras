namespace Application.ListaCompras.DTOs.Historico
{
    public sealed class HistoricoResponseDto
    {
        public int Id { get; set; }
        public string Registro { get; set; } = default!;
        public int ProdutoId { get; set; }
        public int MercadoId { get; set; }
        public int PrecoId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
