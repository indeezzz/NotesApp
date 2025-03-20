using Core.Models;
using Core.Interfaces;

namespace Application.UseCases
{
    public class CreateNoteUseCase
    {
        private readonly INotesRepository _repository;

        public CreateNoteUseCase(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(string name, string body, string image)
        {
            var note = new Notes { Name = name, Body = body, Image = image };
            await _repository.AddAsync(note);
        }
    }
}
