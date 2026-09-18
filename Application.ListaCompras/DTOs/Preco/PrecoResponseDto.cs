namespace Application.ListaCompras.DTOs.Preco
{
    public sealed class PrecoResponseDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
    }
}
