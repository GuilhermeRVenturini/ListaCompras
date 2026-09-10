using Infrastructure.ListaCompras.Common.Results;
using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        private readonly ListaCompras_DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ListaCompras_DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    }
}
