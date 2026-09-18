namespace Application.ListaCompras.DTOs.Preco
{
    public sealed class PrecoRequestDto
    {
        public decimal Valor { get; init; }
        public bool Desconto { get; init; }
    }
}
