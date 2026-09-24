using Microsoft.EntityFrameworkCore;
using Domain.ListaCompras.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public DbSet<UsuarioLista>  UsuarioLista { get; set; }
        public DbSet<ProdutoLista> ProdutoLista { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PrecoMercado>()
                .HasKey(x => new { x.ProdutoId, x.MercadoId });

            modelBuilder.Entity<UsuarioLista>()
                .HasKey(x => new { x.ListaId, x.UsuarioId });

            modelBuilder.Entity<ProdutoLista>()
                .HasKey(x => new { x.ProdutoId, x.ListaId });
        }

    }
}
