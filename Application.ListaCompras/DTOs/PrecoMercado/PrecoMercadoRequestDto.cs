namespace Application.ListaCompras.DTOs.PrecoMercado
{
    public sealed class PrecoMercadoRequestDto
    {
        public int ProdutoId { get; set; }
        public int MercadoId { get; set; }
        public int PrecoId { get; set; }
    }
}
