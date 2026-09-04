using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Lista
    {
        //Properties
        [Key]
        public int Id { get; set; }
        public DateTime DataListaCriacao { get; set; }

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;
    }
}
