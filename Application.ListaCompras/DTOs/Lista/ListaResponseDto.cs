namespace Application.ListaCompras.DTOs.Lista
{
    public sealed class ListaResponseDto
    {
        public int Id { get; set; }
        public DateTime DataListaCriacao { get; set; }
        public string Nome { get; set; } = default!;
    }
}
