using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class DeleteNoteUseCase
    {
        private readonly INotesRepository _repository;

        public DeleteNoteUseCase(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
