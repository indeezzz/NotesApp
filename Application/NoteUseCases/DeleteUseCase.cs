using Core.Interfaces;

namespace Application.UseCases
{
    public class DeleteUseCase<TModel> : IDeleteUseCase<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public DeleteUseCase(IRepository<TModel> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
