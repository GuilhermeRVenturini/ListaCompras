using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Produto
    {
        //Properties
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Nome { get; set; } = default!;
        [Required]
        [MaxLength(1024)]
        public string Descricao { get; set; } = default!;
        [Required]
        [MaxLength(128)]
        public string Fabricante { get; set; } = default!;
        [Required]
        public DateOnly Validade { get; set; }
    }
}
