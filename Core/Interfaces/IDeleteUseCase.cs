using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDeleteUseCase<TModel> where TModel : class
    {
        Task DeleteAsync(Guid id);
    }
}
