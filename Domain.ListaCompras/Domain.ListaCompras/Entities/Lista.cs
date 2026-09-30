using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Lista
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataListaCriacao { get; set; }

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; } = default!;

        public ICollection<ProdutoLista> ProdutoListas { get; set; } = new List<ProdutoLista>();
        public ICollection<UsuarioLista> UsuarioListas { get; set; } = new List<UsuarioLista>();
    }
}
