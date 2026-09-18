using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;
using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public sealed class ProdutoListaRepository : RepositoryBase<ProdutoLista>, IProdutoListaRepository
    {
        public ProdutoListaRepository(ListaCompras_DbContext context) : base(context)
        {
        }

        public async Task<ResultData<IEnumerable<ProdutoLista>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<ProdutoLista>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<ProdutoLista>>.Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<ProdutoLista>> GetByIdAsync(int produtoId, int listaId)
        {
            try
            {
                var entity = await _dbSet.FindAsync(produtoId, listaId);

                if (entity is null)
                    return ResultData<ProdutoLista>.Error("Registro não encontrado.");

                return ResultData<ProdutoLista>.Success(entity);
            }
            catch (Exception)
            {
                return ResultData<ProdutoLista>.Error("Erro ao buscar o registro.");
            }
        }

        public async Task<ResultData<ProdutoLista>> CreateAsync(ProdutoLista entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>.Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>.Error("Erro ao adicionar o registro.");
            }
        }

        public async Task<ResultData<ProdutoLista>> UpdateAsync(ProdutoLista entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>.Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>.Error("Erro ao atualizar o registro.");
            }
        }

        public async Task<ResultData<ProdutoLista>> DeleteAsync(int produtoId, int listaId)
        {
            try
            {
                var result = await GetByIdAsync(produtoId, listaId);

                if (!result.IsSuccess || result.Data is null)
                    return result;

                _dbSet.Remove(result.Data);
                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>.Success(result.Data);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>.Error("Erro ao excluir o registro.");
            }
        }
    }
}
