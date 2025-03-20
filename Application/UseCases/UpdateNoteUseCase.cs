using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UpdateNoteUseCase
    {
        private readonly INotesRepository _repository;

        public UpdateNoteUseCase(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Notes note)
        {
            await _repository.UpdateAsync(note);
        }
    }
}
