using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ListaCompras.Entities
{
    public sealed class Preco
    {
        //Properties
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")] //decimal de 12 de comprimento, 10 é antes da virgula
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
    }
}
