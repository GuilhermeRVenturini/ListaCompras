namespace Application.ListaCompras.DTOs.Produto
{
    public sealed class ProdutoResponseDto
    {
        public int Id { get; init; }
        public string Nome { get; init; } = default!;
        public string Descricao { get; init; } = default!;
        public string Fabricante { get; init; } = default!;
        public DateOnly Validade { get; init; }
    }
}
