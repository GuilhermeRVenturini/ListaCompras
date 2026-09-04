using Microsoft.EntityFrameworkCore;
using Domain.ListaCompras.Entities;

namespace Infrastructure.ListaCompras.Data
{
    public class ListaCompras_DbContext : DbContext
    {
        public ListaCompras_DbContext(DbContextOptions<ListaCompras_DbContext> options) : base(options) { }

        public DbSet<Lista> Lista { get; set; }
        public DbSet<Historico> Historico { get; set; }
        public DbSet<Mercado> Mercado { get; set; }
        public DbSet<Preco> Preco { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PrecoMercado> PrecoMercado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PrecoMercado>().HasNoKey();
        }

    }
}
