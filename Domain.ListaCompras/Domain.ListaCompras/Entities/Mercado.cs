using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Mercado
    {
        //Properties
        [Key]
        public int Id { get; set; }
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
