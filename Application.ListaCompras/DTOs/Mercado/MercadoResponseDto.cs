namespace Application.ListaCompras.DTOs.Mercado
{
    public sealed class MercadoResponseDto
    {
        public int Id { get; init; }
        public string Nome { get; init; } = default!;
        public string HorarioFuncionamento { get; init; } = default!;
        public string Endereco { get; init; } = default!;
    }
}
