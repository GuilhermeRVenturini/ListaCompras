namespace Application.ListaCompras.DTOs.UsuarioLista
{
    public sealed class UsuarioListaResponseDto
    {
        public int ListaId { get; init; }
        public string ListaNome { get; init; } = string.Empty;
        public Guid UsuarioId { get; init; }
        public string UsuarioNome { get; init; } = string.Empty;
        public string UsuarioEmail { get; init; } = string.Empty;
    }
}
