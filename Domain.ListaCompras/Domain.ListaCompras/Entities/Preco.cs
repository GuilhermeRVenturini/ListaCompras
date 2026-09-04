namespace Domain.ListaCompras.Entities
{
    public class Preco
    {
        //Properties
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public bool Desconto { get; set; }
        public decimal ValorAtual { get; set; }
    }
}
