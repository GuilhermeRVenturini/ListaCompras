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
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<PrecoMercado>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<PrecoMercado>>.Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<IEnumerable<PrecoMercado>>> GetByProdutoIdAsync(int produtoId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .Where(x => x.ProdutoId == produtoId)
                    .ToListAsync();

                return ResultData<IEnumerable<PrecoMercado>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<PrecoMercado>>.Error("Erro ao buscar os preços do produto.");
            }
        }

        public async Task<ResultData<IEnumerable<PrecoMercado>>> GetByMercadoIdAsync(int mercadoId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .Where(x => x.MercadoId == mercadoId)
                    .ToListAsync();

                return ResultData<IEnumerable<PrecoMercado>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<PrecoMercado>>.Error("Erro ao buscar os produtos do mercado.");
            }
        }

        public async Task<ResultData<PrecoMercado>> GetByIdAsync(int produtoId, int mercadoId)
        {
            try
            {
                var entity = await QueryWithRelationships()
                    .FirstOrDefaultAsync(x => x.ProdutoId == produtoId && x.MercadoId == mercadoId);

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
                return ResultData<PrecoMercado>.Error("Erro ao vincular o preço ao produto e ao mercado.");
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
                return ResultData<PrecoMercado>.Error("Erro ao atualizar o preço do produto no mercado.");
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
                return ResultData<PrecoMercado>.Error("Erro ao remover o preço do produto no mercado.");
            }
        }

        private IQueryable<PrecoMercado> QueryWithRelationships()
        {
            return _dbSet
                .Include(x => x.Produto)
                .Include(x => x.Mercado)
                .Include(x => x.Preco);
        }
    }
}
