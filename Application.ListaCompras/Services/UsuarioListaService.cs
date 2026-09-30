using Application.ListaCompras.DTOs.UsuarioLista;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class UsuarioListaService : IUsuarioListaService
    {
        private readonly IUsuarioListaRepository _repository;
        private readonly IRepository<Lista, int> _listaRepository;
        private readonly IRepository<Usuario, Guid> _usuarioRepository;

        public UsuarioListaService(
            IUsuarioListaRepository repository,
            IRepository<Lista, int> listaRepository,
            IRepository<Usuario, Guid> usuarioRepository)
        {
            _repository = repository;
            _listaRepository = listaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResultData<IEnumerable<UsuarioListaResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<UsuarioListaResponseDto>>> GetByListaAsync(int listaId)
        {
            var listaResult = await _listaRepository.GetByIdAsync(listaId);

            if (!listaResult.IsSuccess || listaResult.Data is null)
                return ResultData<IEnumerable<UsuarioListaResponseDto>>.Error("Lista não encontrada.");

            var result = await _repository.GetByListaIdAsync(listaId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<UsuarioListaResponseDto>>> GetByUsuarioAsync(Guid usuarioId)
        {
            var usuarioResult = await _usuarioRepository.GetByIdAsync(usuarioId);

            if (!usuarioResult.IsSuccess || usuarioResult.Data is null)
                return ResultData<IEnumerable<UsuarioListaResponseDto>>.Error("Usuário não encontrado.");

            var result = await _repository.GetByUsuarioIdAsync(usuarioId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> GetByIdAsync(int listaId, Guid usuarioId)
        {
            var result = await _repository.GetByIdAsync(listaId, usuarioId);
            return MapResult(result);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> CreateAsync(int listaId, Guid usuarioId)
        {
            var listaResult = await _listaRepository.GetByIdAsync(listaId);
            if (!listaResult.IsSuccess || listaResult.Data is null)
                return ResultData<UsuarioListaResponseDto>.Error("Lista não encontrada.");

            var usuarioResult = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (!usuarioResult.IsSuccess || usuarioResult.Data is null)
                return ResultData<UsuarioListaResponseDto>.Error("Usuário não encontrado.");

            var existingResult = await _repository.GetByIdAsync(listaId, usuarioId);
            if (existingResult.IsSuccess && existingResult.Data is not null)
                return ResultData<UsuarioListaResponseDto>.Error("O usuário já está vinculado a esta lista.");

            var entity = UsuarioListaMapping.ToEntity(listaId, usuarioId);
            entity.Lista = listaResult.Data;
            entity.Usuario = usuarioResult.Data;

            var result = await _repository.CreateAsync(entity);
            return MapResult(result);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> DeleteAsync(int listaId, Guid usuarioId)
        {
            var result = await _repository.DeleteAsync(listaId, usuarioId);
            return MapResult(result);
        }

        private static ResultData<IEnumerable<UsuarioListaResponseDto>> MapCollectionResult(
            ResultData<IEnumerable<UsuarioLista>> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<UsuarioListaResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(UsuarioListaMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<UsuarioListaResponseDto>>.Success(response);
        }

        private static ResultData<UsuarioListaResponseDto> MapResult(ResultData<UsuarioLista> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<UsuarioListaResponseDto>.Error(result.Message);

            return ResultData<UsuarioListaResponseDto>.Success(
                UsuarioListaMapping.ToResponseDto(result.Data));
        }
    }
}
