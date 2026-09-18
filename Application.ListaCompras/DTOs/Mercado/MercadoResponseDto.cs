namespace Application.ListaCompras.DTOs.Mercado
{
    public sealed class MercadoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string HorarioFuncionamento { get; set; } = default!;
        public string Endereco { get; set; } = default!;
    }
}
