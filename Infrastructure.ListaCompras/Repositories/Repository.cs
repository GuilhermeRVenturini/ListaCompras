using Domain.ListaCompras.Common.Results;
using Infrastructure.ListaCompras.Data;
using Domain.ListaCompras.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.ListaCompras.Repositories
{

    public sealed class Repository<T, TId>
        : RepositoryBase<T>, IRepository<T, TId>
        where T : class
    {

        public Repository(ListaCompras_DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Monta uma consulta de leitura sem rastreamento e, se informado, aplica
        /// o predicado antes do <c>ToListAsync</c>. Assim, a filtragem ocorre
        /// na consulta ao banco, não após carregar todos os registros.
        /// </summary>
        public async Task<ResultData<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? filtro = null)
        {
            try
            {

                IQueryable<T> query = _dbSet.AsNoTracking();

                if (filtro is not null)
                    query = query.Where(filtro);

                var entities = await query.ToListAsync();

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

        /// <summary>
        /// Persiste uma entidade usando o rastreamento do Entity Framework.
        /// Quando a entidade já está rastreada, salva as alterações detectadas;
        /// quando está desanexada, chama <c>Update</c> para marcar sua atualização.
        /// </summary>
        public async Task<ResultData<T>> UpdateAsync(T entity)
        {
            try
            {

                if (_context.Entry(entity).State == EntityState.Detached)
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

                var result = await GetByIdAsync(id);

                if (!result.IsSuccess || result.Data is null)
                    return result;

                _dbSet.Remove(result.Data);

                await _context.SaveChangesAsync();

                return ResultData<T>
                    .Success(result.Data);
            }
            catch (DbUpdateException)
            {
                return ResultData<T>
                    .Error("Erro ao excluir o registro.");
            }
        }
    }
}
