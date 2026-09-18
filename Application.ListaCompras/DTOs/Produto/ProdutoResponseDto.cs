namespace Application.ListaCompras.DTOs.Produto
{
    public sealed class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Fabricante { get; set; } = default!;
        public DateOnly Validade { get; set; }
    }
}
