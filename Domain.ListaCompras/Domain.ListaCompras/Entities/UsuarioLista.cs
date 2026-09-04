namespace Domain.ListaCompras.Entities
{
    public class UsuarioLista
    {
        public int ListaId { get; set; }
        public virtual Lista? Lista { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
