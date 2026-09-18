using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Usuario
{
    public sealed class UsuarioRequestDto
    {
        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        public string Email { get; set; } = default!;
    }
}
