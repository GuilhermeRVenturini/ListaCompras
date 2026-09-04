namespace Domain.ListaCompras.Entities
{
    public class Produto
    {
        //Properties
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Fabricante { get; set; } = default!;
        public DateOnly Validade { get; set; }
    }
}
