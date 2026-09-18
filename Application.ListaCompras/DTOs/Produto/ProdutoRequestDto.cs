using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Produto
{
    public sealed class ProdutoRequestDto
    {
        [Required]
        [MaxLength(128)]
        public string Nome { get; set; } = default!;

        [Required]
        [MaxLength(1024)]
        public string Descricao { get; set; } = default!;

        [Required]
        [MaxLength(128)]
        public string Fabricante { get; set; } = default!;

        public DateOnly Validade { get; set; }
    }
}
