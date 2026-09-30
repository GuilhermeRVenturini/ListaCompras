namespace Application.ListaCompras.DTOs.Historico
{
    public sealed class HistoricoResponseDto
    {
        public int Id { get; init; }
        public string Entidade { get; init; } = string.Empty;
        public string Operacao { get; init; } = string.Empty;
        public string Registro { get; init; } = string.Empty;
        public DateTime DataRegistro { get; init; }
    }
}
