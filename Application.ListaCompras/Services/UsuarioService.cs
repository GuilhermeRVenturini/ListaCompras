using Application.ListaCompras.DTOs.Usuario;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

// usamos async e await pq algumas coisas demoram
// tipo buscar ou salvar algo no banco
// ai enquanto esperamos a operação terminar
// n precisamos ficar travando a thread atoa
// quando termina o codigo continua normalmente
namespace Application.ListaCompras.Services
{

    public sealed class UsuarioService : IUsuarioService
    {

        private readonly IRepository<Usuario, Guid> _repository;

        // aqui faço a operação e trato o retorno antes de passar pra API
        public UsuarioService(IRepository<Usuario, Guid> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
        public async Task<ResultData<IEnumerable<UsuarioResponseDto>>> GetAllAsync(string? nome = null)
        {

            // primeiro pego todos e depois aplico o filtro, então por enquanto o banco ainda manda tudo
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<UsuarioResponseDto>>.Error(result.Message);

            var usuarios = result.Data;

            // só filtra se digitar um nome. "bru" já serve pra achar "Bruno"
            if (!string.IsNullOrWhiteSpace(nome))
            {
                // ignora maiúscula/minúscula e tira espaços sobrando da busca
                usuarios = usuarios.Where(u =>
                    u.Nome.Contains(nome.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            var response = usuarios
                .Select(UsuarioMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<UsuarioResponseDto>>.Success(response);
        }

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<UsuarioResponseDto>> GetByIdAsync(Guid id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // cria um registro novo e devolve o resultado
        public async Task<ResultData<UsuarioResponseDto>> CreateAsync(UsuarioRequestDto request)
        {

            var entity = UsuarioMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        // acha o registro e atualiza os dados dele
        public async Task<ResultData<UsuarioResponseDto>> UpdateAsync(Guid id, UsuarioRequestDto request)
        {

            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<UsuarioResponseDto>.Error(existingResult.Message);

            UsuarioMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        // remove o registro usando o id
        public async Task<ResultData<UsuarioResponseDto>> DeleteAsync(Guid id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<UsuarioResponseDto> MapResult(ResultData<Usuario> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<UsuarioResponseDto>.Error(result.Message);

            return ResultData<UsuarioResponseDto>.Success(
                UsuarioMapping.ToResponseDto(result.Data));
        }
    }
}
