using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Status
{
    public sealed class StatusRequestDto
    {
        [Required]
        [MaxLength(128)]
        public string Nome { get; set; } = default!;
    }
}
