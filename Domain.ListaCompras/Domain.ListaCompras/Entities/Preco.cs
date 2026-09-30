using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ListaCompras.Entities
{
    public sealed class Preco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        public bool Desconto { get; set; }

        public ICollection<PrecoMercado> PrecoMercados { get; set; } = new List<PrecoMercado>();
        public ICollection<Historico> Historicos { get; set; } = new List<Historico>();
    }
}
