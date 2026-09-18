namespace Application.ListaCompras.DTOs.ProdutoLista
{
    public sealed class ProdutoListaResponseDto
    {
        public int ProdutoId { get; set; }
        public int ListaId { get; set; }
        public int StatusId { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
