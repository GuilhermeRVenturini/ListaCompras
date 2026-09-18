using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Lista
{
    public sealed class ListaRequestDto
    {
        public DateTime DataListaCriacao { get; init; }

        [Required]
        [MaxLength(64)]
        public string Nome { get; init; } = default!;
    }
}
