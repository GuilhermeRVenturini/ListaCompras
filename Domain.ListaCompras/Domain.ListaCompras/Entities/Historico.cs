using System.ComponentModel.DataAnnotations;

namespace Domain.ListaCompras.Entities
{
    public sealed class Historico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Entidade { get; set; } = default!;

        [Required]
        [MaxLength(32)]
        public string Operacao { get; set; } = default!;

        [Required]
        [MaxLength(1024)]
        public string Registro { get; set; } = default!;

        public int? ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public int? ListaId { get; set; }
        public Lista? Lista { get; set; }

        public Guid? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? MercadoId { get; set; }
        public Mercado? Mercado { get; set; }

        public int? PrecoId { get; set; }
        public Preco? Preco { get; set; }

        public int? StatusId { get; set; }
        public Status? Status { get; set; }

        public DateTime DataRegistro { get; set; }
    }
}
