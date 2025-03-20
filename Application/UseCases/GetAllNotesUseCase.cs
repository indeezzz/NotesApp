using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllNotesUseCase
    {
        private readonly INotesRepository _repository;

        public GetAllNotesUseCase(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notes>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
