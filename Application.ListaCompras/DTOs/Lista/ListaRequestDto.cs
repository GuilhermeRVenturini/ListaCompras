using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Lista
{
    public sealed class ListaRequestDto
    {
        public DateTime DataListaCriacao { get; set; }

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;
    }
}
