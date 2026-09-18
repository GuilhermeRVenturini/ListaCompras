namespace Application.ListaCompras.DTOs.ProdutoLista
{
    public sealed class ProdutoListaRequestDto
    {
        public int ProdutoId { get; init; }
        public int ListaId { get; init; }
        public int StatusId { get; init; }
        public int QuantidadeEstoque { get; init; }
    }
}
