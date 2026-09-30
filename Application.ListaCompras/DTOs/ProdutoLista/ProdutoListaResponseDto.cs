namespace Application.ListaCompras.DTOs.ProdutoLista
{
    public sealed class ProdutoListaResponseDto
    {
        public int ProdutoId { get; init; }
        public string ProdutoNome { get; init; } = string.Empty;
        public int ListaId { get; init; }
        public string ListaNome { get; init; } = string.Empty;
        public int StatusId { get; init; }
        public string StatusNome { get; init; } = string.Empty;
        public int QuantidadeEstoque { get; init; }
    }
}
