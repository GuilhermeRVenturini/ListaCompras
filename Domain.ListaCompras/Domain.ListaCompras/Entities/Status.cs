using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Status
    {
        //Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Nome { get; set; } = default!;
    }
}
