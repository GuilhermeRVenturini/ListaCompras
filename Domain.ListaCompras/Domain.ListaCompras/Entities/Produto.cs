using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Produto
    {
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

        public ICollection<ProdutoLista> ProdutoListas { get; set; } = new List<ProdutoLista>();
        public ICollection<PrecoMercado> PrecoMercados { get; set; } = new List<PrecoMercado>();
        public ICollection<Historico> Historicos { get; set; } = new List<Historico>();
    }
}
