using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Mercado
{
    public sealed class MercadoRequestDto
    {
        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;

        [Required]
        [MaxLength(256)]
        public string HorarioFuncionamento { get; set; } = default!;

        [Required]
        [MaxLength(256)]
        public string Endereco { get; set; } = default!;
    }
}
