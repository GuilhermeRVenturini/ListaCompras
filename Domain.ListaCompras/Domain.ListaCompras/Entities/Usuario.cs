using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        public string Email { get; set; } = default!;

        public ICollection<UsuarioLista> UsuarioListas { get; set; } = new List<UsuarioLista>();
    }
}
