using Domain.ListaCompras.Common.Results;
using Infrastructure.ListaCompras.Data;
using Domain.ListaCompras.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public sealed class Repository<T, TId> : RepositoryBase<T>, IRepository<T, TId>
        where T : class
    {
        public Repository(ListaCompras_DbContext context) : base(context)
        {}

        public async Task<ResultData<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<T>>
                    .Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<T>>
                    .Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<T>> GetByIdAsync(TId id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity is null)
                    return ResultData<T>
                        .Error("Registro não encontrado.");

                return ResultData<T>
                    .Success(entity);
            }
            catch (Exception)
            {
                return ResultData<T>
                    .Error("Erro ao buscar o registro.");
            }
        }

        public async Task<ResultData<T>> CreateAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                return ResultData<T>
                    .Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<T>
                    .Error("Erro ao adicionar o registro.");
            }
        }

        public async Task<ResultData<T>> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return ResultData<T>
                    .Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<T>
                    .Error("Erro ao atualizar o registro.");
            }
        }

        public async Task<ResultData<T>> DeleteAsync(TId id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity is null)
                    return ResultData<T>
                        .Error("Registro não encontrado.");

                _dbSet.Remove(entity);

                await _context.SaveChangesAsync();

                return ResultData<T>
                    .Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<T>
                    .Error("Erro ao excluir o registro.");
            }
        }
    }
}