using Application.ListaCompras.Interfaces;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class MercadoService : IMercadoService
    {
        private readonly IRepository<Mercado, int> _repository;

        public MercadoService(IRepository<Mercado, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<Mercado>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ResultData<Mercado>> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ResultData<Mercado>> CreateAsync(Mercado mercado)
        {
            return await _repository.CreateAsync(mercado);
        }

        public async Task<ResultData<Mercado>> UpdateAsync(Mercado mercado)
        {
            return await _repository.UpdateAsync(mercado);
        }

        public async Task<ResultData<Mercado>> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}