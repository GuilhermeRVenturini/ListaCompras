namespace Domain.ListaCompras.Entities
{
    public class Mercado
    {
        //Properties
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string HorarioFuncionamento { get; set; } = default!;
        public string Endereco { get; set; } = default!;
    }
}
