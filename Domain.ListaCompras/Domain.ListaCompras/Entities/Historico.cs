namespace Domain.ListaCompras.Entities
{
    public class Historico
    {
        //Properties
        public int Id { get; set; }
        public string Registro { get; set; } = default!; //vai ter que ter algo

        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public int MercadoId { get; set; }
        public virtual Mercado? Mercado { get; set; }

        public int PrecoId { get; set; }
        public virtual Preco? Preco { get; set; }
    }
}
