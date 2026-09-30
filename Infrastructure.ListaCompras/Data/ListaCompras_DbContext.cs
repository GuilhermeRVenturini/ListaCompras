using Domain.ListaCompras.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public DbSet<UsuarioLista> UsuarioLista { get; set; }
        public DbSet<ProdutoLista> ProdutoLista { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProdutoLista>(entity =>
            {
                entity.HasKey(x => new { x.ProdutoId, x.ListaId });

                entity.HasOne(x => x.Produto)
                    .WithMany(x => x.ProdutoListas)
                    .HasForeignKey(x => x.ProdutoId);

                entity.HasOne(x => x.Lista)
                    .WithMany(x => x.ProdutoListas)
                    .HasForeignKey(x => x.ListaId);

                entity.HasOne(x => x.Status)
                    .WithMany(x => x.ProdutoListas)
                    .HasForeignKey(x => x.StatusId);
            });

            modelBuilder.Entity<UsuarioLista>(entity =>
            {
                entity.HasKey(x => new { x.ListaId, x.UsuarioId });

                entity.HasOne(x => x.Lista)
                    .WithMany(x => x.UsuarioListas)
                    .HasForeignKey(x => x.ListaId);

                entity.HasOne(x => x.Usuario)
                    .WithMany(x => x.UsuarioListas)
                    .HasForeignKey(x => x.UsuarioId);
            });

            modelBuilder.Entity<PrecoMercado>(entity =>
            {
                entity.HasKey(x => new { x.ProdutoId, x.MercadoId });

                entity.HasOne(x => x.Produto)
                    .WithMany(x => x.PrecoMercados)
                    .HasForeignKey(x => x.ProdutoId);

                entity.HasOne(x => x.Mercado)
                    .WithMany(x => x.PrecoMercados)
                    .HasForeignKey(x => x.MercadoId);

                entity.HasOne(x => x.Preco)
                    .WithMany(x => x.PrecoMercados)
                    .HasForeignKey(x => x.PrecoId);
            });

            modelBuilder.Entity<Historico>(entity =>
            {
                entity.HasOne(x => x.Produto)
                    .WithMany(x => x.Historicos)
                    .HasForeignKey(x => x.ProdutoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Mercado)
                    .WithMany(x => x.Historicos)
                    .HasForeignKey(x => x.MercadoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Preco)
                    .WithMany(x => x.Historicos)
                    .HasForeignKey(x => x.PrecoId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Lista)
                    .WithMany()
                    .HasForeignKey(x => x.ListaId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Usuario)
                    .WithMany()
                    .HasForeignKey(x => x.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Status)
                    .WithMany()
                    .HasForeignKey(x => x.StatusId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Nome = "Pendente" },
                new Status { Id = 2, Nome = "Comprado" },
                new Status { Id = 3, Nome = "Indisponível" }
            );
        }

        // Antes de salvar, registra de forma simples o que mudou no contexto.
        public override int SaveChanges()
        {
            GerarHistorico();
            return base.SaveChanges();
        }

        // Versão assíncrona usada pelos repositórios.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GerarHistorico();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void GerarHistorico()
        {
            ChangeTracker.DetectChanges();

            var alteracoes = ChangeTracker.Entries()
                .Where(entry =>
                    entry.Entity is not Domain.ListaCompras.Entities.Historico &&
                    (entry.State == EntityState.Added ||
                     entry.State == EntityState.Modified ||
                     entry.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in alteracoes)
            {
                string operacao;

                if (entry.State == EntityState.Added)
                    operacao = "CRIADO";
                else if (entry.State == EntityState.Modified)
                    operacao = "ATUALIZADO";
                else
                    operacao = "REMOVIDO";

                Historico.Add(new Domain.ListaCompras.Entities.Historico
                {
                    Entidade = entry.Metadata.ClrType.Name,
                    Operacao = operacao,
                    Registro = $"{operacao} - {entry.Metadata.ClrType.Name}",
                    DataRegistro = DateTime.UtcNow
                });
            }
        }

    }
}
