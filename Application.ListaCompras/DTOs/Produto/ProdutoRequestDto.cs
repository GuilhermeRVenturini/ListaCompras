using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Produto
{
    public sealed class ProdutoRequestDto
    {
        [Required]
        [MaxLength(128)]
        public string Nome { get; init; } = default!;

        [Required]
        [MaxLength(1024)]
        public string Descricao { get; init; } = default!;

        [Required]
        [MaxLength(128)]
        public string Fabricante { get; init; } = default!;

        public DateOnly Validade { get; init; }
    }
}
