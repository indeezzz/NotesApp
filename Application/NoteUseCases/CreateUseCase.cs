using Core.Models;
using Core.Interfaces;

namespace Application.UseCases
{
    public class CreateUseCase<TModel> : ICreateUseCase<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public CreateUseCase(IRepository<TModel> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TModel model)
        {
            await _repository.AddAsync(model);
        }
    }
}
