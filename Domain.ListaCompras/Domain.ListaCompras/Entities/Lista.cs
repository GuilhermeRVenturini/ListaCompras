using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public class Lista
    {
        //Properties
        [Key]
        public int Id { get; set; }
        public DateTime DataListaCriacao { get; set; }
        public string Nome { get; set; } = default!;
    }
}
