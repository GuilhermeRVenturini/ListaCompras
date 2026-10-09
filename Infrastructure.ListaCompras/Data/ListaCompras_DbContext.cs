using Domain.ListaCompras.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.ListaCompras.Data
{

    public class ListaCompras_DbContext : DbContext
    {

        public ListaCompras_DbContext(
            DbContextOptions<ListaCompras_DbContext> options)
            : base(options)
        {
        }

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

        /// <summary>
        /// Define os relacionamentos que não ficam evidentes apenas pelas entidades:
        /// chaves compostas dos vínculos produto/lista, usuário/lista e preço/mercado;
        /// referências do histórico com exclusão configurada como <c>SetNull</c>;
        /// e os status iniciais cadastrados por seed.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProdutoLista>(entity =>
            {

                entity.HasKey(x => new
                {
                    x.ProdutoId,
                    x.ListaId
                });

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

                entity.HasKey(x => new
                {
                    x.ListaId,
                    x.UsuarioId
                });

                entity.HasOne(x => x.Lista)
                    .WithMany(x => x.UsuarioListas)
                    .HasForeignKey(x => x.ListaId);

                entity.HasOne(x => x.Usuario)
                    .WithMany(x => x.UsuarioListas)
                    .HasForeignKey(x => x.UsuarioId);
            });

            modelBuilder.Entity<PrecoMercado>(entity =>
            {

                entity.HasKey(x => new
                {
                    x.ProdutoId,
                    x.MercadoId
                });

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
                new Status
                {
                    Id = 1,
                    Nome = "Pendente"
                },
                new Status
                {
                    Id = 2,
                    Nome = "Comprado"
                },
                new Status
                {
                    Id = 3,
                    Nome = "Indisponível"
                }
            );
        }

        public override int SaveChanges()
        {
            GerarHistorico();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            GerarHistorico();

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Prepara os registros de histórico das alterações rastreadas pelo EF Core.
        /// <list type="number">
        /// <item>Identifica entidades criadas, alteradas ou removidas, sem incluir o próprio <c>Historico</c>.</item>
        /// <item>Obtém o nome da entidade e os valores de sua chave primária; em exclusões, usa os valores originais.</item>
        /// <item>Adiciona um evento com operação, identificação e horário em UTC para cada entrada encontrada.</item>
        /// </list>
        /// Os eventos são incluídos antes de <c>SaveChanges</c> e persistidos junto
        /// com as demais mudanças, sem gerar histórico dos próprios eventos.
        /// </summary>
        private void GerarHistorico()
        {

            ChangeTracker.DetectChanges();

            var alteracoes = ChangeTracker.Entries()
                .Where(entry =>

                    entry.Entity
                        is not Domain.ListaCompras.Entities.Historico &&

                    (entry.State == EntityState.Added ||
                     entry.State == EntityState.Modified ||
                     entry.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in alteracoes)
            {
                string operacao;

                if (entry.State == EntityState.Added)
                {
                    operacao = "CRIADO";
                }
                else if (entry.State == EntityState.Modified)
                {
                    operacao = "ATUALIZADO";
                }
                else
                {
                    operacao = "REMOVIDO";
                }

                string entidade = entry.Metadata.ClrType.Name;

                var chaves = entry.Metadata.FindPrimaryKey()?.Properties;

                string registro = entidade;

                if (chaves != null)
                {
                    var ids = chaves.Select(chave =>
                    {
                        var propriedade = entry.Property(chave.Name);

                        var valor = entry.State == EntityState.Deleted
                            ? propriedade.OriginalValue
                            : propriedade.CurrentValue;

                        return $"{chave.Name}: {valor}";
                    });

                    registro = $"{entidade} - {string.Join(", ", ids)}";
                }

                Historico.Add(
                    new Domain.ListaCompras.Entities.Historico
                    {
                        Entidade = entidade,
                        Operacao = operacao,
                        Registro = registro,
                        DataRegistro = DateTime.UtcNow
                    });
            }
        }
    }
}
