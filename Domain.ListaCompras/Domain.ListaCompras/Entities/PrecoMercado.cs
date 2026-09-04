namespace Domain.ListaCompras.Entities
{
    public class PrecoMercado
    {
        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public int MercadoId { get; set; }
        public virtual Mercado? Mercado { get; set; }

        public int PrecoId { get; set; }
        public virtual Preco? Preco { get; set; }
    }
}
