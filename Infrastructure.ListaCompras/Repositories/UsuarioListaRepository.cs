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
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .ToListAsync();

                return ResultData<IEnumerable<UsuarioLista>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<UsuarioLista>>.Error("Erro ao buscar os registros.");
            }
        }

        public async Task<ResultData<IEnumerable<UsuarioLista>>> GetByListaIdAsync(int listaId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .Where(x => x.ListaId == listaId)
                    .ToListAsync();

                return ResultData<IEnumerable<UsuarioLista>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<UsuarioLista>>.Error("Erro ao buscar os usuários da lista.");
            }
        }

        public async Task<ResultData<IEnumerable<UsuarioLista>>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            try
            {
                var entities = await QueryWithRelationships()
                    .AsNoTracking()
                    .Where(x => x.UsuarioId == usuarioId)
                    .ToListAsync();

                return ResultData<IEnumerable<UsuarioLista>>.Success(entities);
            }
            catch (Exception)
            {
                return ResultData<IEnumerable<UsuarioLista>>.Error("Erro ao buscar as listas do usuário.");
            }
        }

        public async Task<ResultData<UsuarioLista>> GetByIdAsync(int listaId, Guid usuarioId)
        {
            try
            {
                var entity = await QueryWithRelationships()
                    .FirstOrDefaultAsync(x => x.ListaId == listaId && x.UsuarioId == usuarioId);

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
                return ResultData<UsuarioLista>.Error("Erro ao adicionar o usuário à lista.");
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
                return ResultData<UsuarioLista>.Error("Erro ao remover o usuário da lista.");
            }
        }

        private IQueryable<UsuarioLista> QueryWithRelationships()
        {
            return _dbSet
                .Include(x => x.Lista)
                .Include(x => x.Usuario);
        }
    }
}
