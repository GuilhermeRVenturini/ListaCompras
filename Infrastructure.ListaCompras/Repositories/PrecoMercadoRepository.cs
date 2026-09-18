using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;
using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public sealed class PrecoMercadoRepository : RepositoryBase<PrecoMercado>, IPrecoMercadoRepository
    {
        public PrecoMercadoRepository(ListaCompras_DbContext context) : base(context)
        {
        }

        public async Task<ResultData<IEnumerable<PrecoMercado>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<PrecoMercado>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<PrecoMercado>>.Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<PrecoMercado>> GetByIdAsync(int produtoId, int mercadoId)
        {
            try
            {
                var entity = await _dbSet.FindAsync(produtoId, mercadoId);

                if (entity is null)
                    return ResultData<PrecoMercado>.Error("Registro não encontrado.");

                return ResultData<PrecoMercado>.Success(entity);
            }
            catch (Exception)
            {
                return ResultData<PrecoMercado>.Error("Erro ao buscar o registro.");
            }
        }

        public async Task<ResultData<PrecoMercado>> CreateAsync(PrecoMercado entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return ResultData<PrecoMercado>.Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<PrecoMercado>.Error("Erro ao adicionar o registro.");
            }
        }

        public async Task<ResultData<PrecoMercado>> UpdateAsync(PrecoMercado entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                return ResultData<PrecoMercado>.Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<PrecoMercado>.Error("Erro ao atualizar o registro.");
            }
        }

        public async Task<ResultData<PrecoMercado>> DeleteAsync(int produtoId, int mercadoId)
        {
            try
            {
                var result = await GetByIdAsync(produtoId, mercadoId);

                if (!result.IsSuccess || result.Data is null)
                    return result;

                _dbSet.Remove(result.Data);
                await _context.SaveChangesAsync();

                return ResultData<PrecoMercado>.Success(result.Data);
            }
            catch (DbUpdateException)
            {
                return ResultData<PrecoMercado>.Error("Erro ao excluir o registro.");
            }
        }
    }
}
