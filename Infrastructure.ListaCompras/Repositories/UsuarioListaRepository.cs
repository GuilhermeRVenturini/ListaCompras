using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;
using Infrastructure.ListaCompras.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ListaCompras.Repositories
{
    public sealed class UsuarioListaRepository : RepositoryBase<UsuarioLista>, IUsuarioListaRepository
    {
        public UsuarioListaRepository(ListaCompras_DbContext context) : base(context)
        {
        }

        public async Task<ResultData<IEnumerable<UsuarioLista>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<UsuarioLista>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<UsuarioLista>>.Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<UsuarioLista>> GetByIdAsync(int listaId, Guid usuarioId)
        {
            try
            {
                var entity = await _dbSet.FindAsync(listaId, usuarioId);

                if (entity is null)
                    return ResultData<UsuarioLista>.Error("Registro não encontrado.");

                return ResultData<UsuarioLista>.Success(entity);
            }
            catch (Exception)
            {
                return ResultData<UsuarioLista>.Error("Erro ao buscar o registro.");
            }
        }

        public async Task<ResultData<UsuarioLista>> CreateAsync(UsuarioLista entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return ResultData<UsuarioLista>.Success(entity);
            }
            catch (DbUpdateException)
            {
                return ResultData<UsuarioLista>.Error("Erro ao adicionar o registro.");
            }
        }

        public async Task<ResultData<UsuarioLista>> DeleteAsync(int listaId, Guid usuarioId)
        {
            try
            {
                var result = await GetByIdAsync(listaId, usuarioId);

                if (!result.IsSuccess || result.Data is null)
                    return result;

                _dbSet.Remove(result.Data);
                await _context.SaveChangesAsync();

                return ResultData<UsuarioLista>.Success(result.Data);
            }
            catch (DbUpdateException)
            {
                return ResultData<UsuarioLista>.Error("Erro ao excluir o registro.");
            }
        }
    }
}
