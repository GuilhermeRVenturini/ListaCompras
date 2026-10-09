using Application.ListaCompras.DTOs.Produto;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;



namespace Application.ListaCompras.Services
{

    public sealed class ProdutoService : IProdutoService
    {

        private readonly IRepository<Produto, int> _repository;

        // aqui faço a operação e trato o retorno antes de passar pra API
        public ProdutoService(IRepository<Produto, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
        public async Task<ResultData<IEnumerable<ProdutoResponseDto>>> GetAllAsync()
        {

            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<ProdutoResponseDto>>.Error(result.Message);

            var response = result.Data

                .Select(ProdutoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<ProdutoResponseDto>>.Success(response);
        }

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<ProdutoResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // cria um registro novo e devolve o resultado
        public async Task<ResultData<ProdutoResponseDto>> CreateAsync(ProdutoRequestDto request)
        {
            // aqui chega o produto que o controller mandou
            // primeiro vemos se ta tudo certo com os dados
            // dps transformamos o dto em produto de verdade
            // mandamos pro repository salvar no banco
            // e no final devolvemos o resultado pro controller

            var entity = ProdutoMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        // acha o registro e atualiza os dados dele
        public async Task<ResultData<ProdutoResponseDto>> UpdateAsync(int id, ProdutoRequestDto request)
        {
            // antes de mandar salvar a gente confere algumas coisas
            // tipo se os ids existem e se a ligação ja n foi criada
            // pq se mandar qualquer coisa direto pro banco
            // pode acabar dando erro ou cadastrando coisa repetida
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ProdutoResponseDto>.Error(existingResult.Message);

            ProdutoMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        // remove o registro usando o id
        public async Task<ResultData<ProdutoResponseDto>> DeleteAsync(int id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<ProdutoResponseDto> MapResult(ResultData<Produto> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<ProdutoResponseDto>.Error(result.Message);

            return ResultData<ProdutoResponseDto>.Success(
                ProdutoMapping.ToResponseDto(result.Data));
        }
    }
}
