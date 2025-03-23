using Core.Interfaces;
using Core.Models;

namespace Application.UseCases
{
    public class GetByIdUseCase<TModel> : IGetByIdUseCase<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public GetByIdUseCase(IRepository<TModel> repository)
        {
            _repository = repository;
        }

        public async Task<TModel?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
