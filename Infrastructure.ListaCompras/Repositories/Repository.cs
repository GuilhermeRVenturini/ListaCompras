using Domain.ListaCompras.Common.Results;
using Infrastructure.ListaCompras.Data;
using Domain.ListaCompras.Interfaces;
using Microsoft.EntityFrameworkCore;

// resumindo o caminho:
// o controller recebe oq a pessoa pediu
// manda pro service que confere as regras
// o service chama o repository pra mexer no banco
// ai o resultado volta pro service e dps pro controller
// que finalmente responde pra quem fez a requisição
namespace Infrastructure.ListaCompras.Repositories
{

    public sealed class Repository<T, TId>
        : RepositoryBase<T>, IRepository<T, TId>
        where T : class
    {

        // método que conversa com o DbContext pra fazer a operação no banco
        public Repository(ListaCompras_DbContext context)
            : base(context)
        {
        }


        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
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


        // procura um registro pelo id, se não achar volta erro
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


        // cria um registro novo e devolve o resultado
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


        // acha o registro e atualiza os dados dele
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


        // remove o registro usando o id
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
