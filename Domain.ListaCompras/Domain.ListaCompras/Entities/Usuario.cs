namespace Domain.ListaCompras.Entities
{
    public class Usuario
    {
        //Properties
        public Guid Id { get; set; }
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
