using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Preco
    {
        //Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
    }
}
