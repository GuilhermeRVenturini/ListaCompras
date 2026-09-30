namespace Application.ListaCompras.DTOs.PrecoMercado
{
    public sealed class PrecoMercadoResponseDto
    {
        public int ProdutoId { get; init; }
        public string ProdutoNome { get; init; } = string.Empty;
        public int MercadoId { get; init; }
        public string MercadoNome { get; init; } = string.Empty;
        public int PrecoId { get; init; }
        public decimal Valor { get; init; }
        public bool Desconto { get; init; }
    }
}
