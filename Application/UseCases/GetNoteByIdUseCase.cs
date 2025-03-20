using Core.Interfaces;
using Core.Models;

namespace Application.UseCases
{
    public class GetNoteByIdUseCase
    {
        private readonly INotesRepository _repository;

        public GetNoteByIdUseCase(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Notes?> ExecuteAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
