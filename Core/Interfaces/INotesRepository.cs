using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotesRepository
    {
        Task AddAsync(Notes note);
        Task<IEnumerable<Notes>> GetAllAsync();
        Task<Notes?> GetByIdAsync(Guid id);
        Task UpdateAsync(Notes note);
        Task DeleteAsync(Guid id);
    }
}
