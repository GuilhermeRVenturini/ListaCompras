namespace Application.ListaCompras.DTOs.PrecoMercado
{
    public sealed class PrecoMercadoRequestDto
    {
        public int ProdutoId { get; init; }
        public int MercadoId { get; init; }
        public int PrecoId { get; init; }
    }
}
