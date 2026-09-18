namespace Application.ListaCompras.DTOs.Lista
{
    public sealed class ListaResponseDto
    {
        public int Id { get; init; }
        public DateTime DataListaCriacao { get; init; }
        public string Nome { get; init; } = default!;
    }
}
