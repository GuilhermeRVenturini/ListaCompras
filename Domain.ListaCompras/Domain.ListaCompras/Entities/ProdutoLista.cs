namespace Domain.ListaCompras.Entities
{
    public class ProdutoLista
    {
        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public int ListaId { get; set; }
        public virtual Lista? Lista { get; set; }

        public int StatusId { get; set; }
        public virtual Status? Status { get; set; }

        public int QuantidadeEstoque { get; set; }
    }
}
