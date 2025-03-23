using Core.Interfaces;
using Core.Models;

namespace Application.UseCases
{
    public class UpdateUseCase<TModel> : IUpdateUseCase<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public UpdateUseCase(IRepository<TModel> repository)
        {
            _repository = repository;
        }

        public async Task UpdateAsync(TModel model)
        {
            await _repository.UpdateAsync(model);
        }
    }
}
