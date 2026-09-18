namespace Application.ListaCompras.DTOs.Preco
{
    public sealed class PrecoResponseDto
    {
        public int Id { get; init; }
        public decimal Valor { get; init; }
        public bool Desconto { get; init; }
    }
}
