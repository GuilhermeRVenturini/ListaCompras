using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public abstract class RepositoryBase<T>
        where T : class
    {
        protected readonly ListaCompras_DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(ListaCompras_DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
    }
}