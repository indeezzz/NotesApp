using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllUseCase<TModel> : IGetAllUseCase<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public GetAllUseCase(IRepository<TModel> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
