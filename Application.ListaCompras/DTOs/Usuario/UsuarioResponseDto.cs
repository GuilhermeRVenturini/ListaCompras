namespace Application.ListaCompras.DTOs.Usuario
{
    public sealed class UsuarioResponseDto
    {
        public Guid Id { get; init; }
        public string Cpf { get; init; } = default!;
        public string Nome { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
