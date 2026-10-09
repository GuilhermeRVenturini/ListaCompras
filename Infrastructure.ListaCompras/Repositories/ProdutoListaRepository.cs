using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;
using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

// esses aqui sao diferentes do repository normal
// pq n da pra achar o registro usando só um id
// tipo no ProdutoLista precisamos do id do produto e da lista
// os dois juntos mostram exatamente qual ligação queremos
// ai usamos isso pra buscar, editar ou excluir
namespace Infrastructure.ListaCompras.Repositories
{

    public sealed class ProdutoListaRepository
        : RepositoryBase<ProdutoLista>, IProdutoListaRepository
    {

        // método que conversa com o DbContext pra fazer a operação no banco
        public ProdutoListaRepository(ListaCompras_DbContext context)
            : base(context)
        {
        }


        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
        public async Task<ResultData<IEnumerable<ProdutoLista>>> GetAllAsync()
        {
            try
            {
                var entities = await QueryWithRelationships()


                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<ProdutoLista>>
                    .Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<ProdutoLista>>
                    .Error("Erro ao buscar os registros.");
            }
        }


        public async Task<ResultData<IEnumerable<ProdutoLista>>> GetByListaIdAsync(
            int listaId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()


                    .Where(x => x.ListaId == listaId)
                    .ToListAsync();

                return ResultData<IEnumerable<ProdutoLista>>
                    .Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<ProdutoLista>>
                    .Error("Erro ao buscar os produtos da lista.");
            }
        }


        public async Task<ResultData<IEnumerable<ProdutoLista>>> GetByProdutoIdAsync(
            int produtoId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()


                    .Where(x => x.ProdutoId == produtoId)
                    .ToListAsync();

                return ResultData<IEnumerable<ProdutoLista>>
                    .Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<ProdutoLista>>
                    .Error("Erro ao buscar as listas do produto.");
            }
        }


        public async Task<ResultData<ProdutoLista>> GetByIdAsync(
            int produtoId,
            int listaId)
        {
            try
            {
                var entity = await QueryWithRelationships()
                    .FirstOrDefaultAsync(
                        x => x.ProdutoId == produtoId &&
                             x.ListaId == listaId);

                if (entity is null)
                    return ResultData<ProdutoLista>
                        .Error("Registro não encontrado.");

                return ResultData<ProdutoLista>
                    .Success(entity);
            }
            catch (Exception)
            {
                return ResultData<ProdutoLista>
                    .Error("Erro ao buscar o registro.");
            }
        }


        public async Task<ResultData<ProdutoLista>> CreateAsync(
            ProdutoLista entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);


                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>
                    .Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>
                    .Error("Erro ao adicionar o produto à lista.");
            }
        }


        public async Task<ResultData<ProdutoLista>> UpdateAsync(
            ProdutoLista entity)
        {
            try
            {
                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>
                    .Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>
                    .Error("Erro ao atualizar o produto da lista.");
            }
        }


        public async Task<ResultData<ProdutoLista>> DeleteAsync(
            int produtoId,
            int listaId)
        {
            try
            {

                var result = await GetByIdAsync(produtoId, listaId);

                if (!result.IsSuccess || result.Data is null)
                    return result;

                _dbSet.Remove(result.Data);

                await _context.SaveChangesAsync();

                return ResultData<ProdutoLista>
                    .Success(result.Data);
            }
            catch (DbUpdateException)
            {
                return ResultData<ProdutoLista>
                    .Error("Erro ao remover o produto da lista.");
            }
        }


        // método que conversa com o DbContext pra fazer a operação no banco
        private IQueryable<ProdutoLista> QueryWithRelationships()
        {
            return _dbSet


                .Include(x => x.Produto)


                .Include(x => x.Lista)


                .Include(x => x.Status);
        }
    }
}
