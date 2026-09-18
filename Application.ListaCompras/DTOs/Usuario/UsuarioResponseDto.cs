namespace Application.ListaCompras.DTOs.Usuario
{
    public sealed class UsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
