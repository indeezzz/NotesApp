using Application.UseCases;
using Core.Models;

namespace ConsoleApplication.Controllers.NotesController
{
    public class NotesDeleteController
    {
        private readonly DeleteUseCase<Notes> _deleteUseCase;

        public NotesDeleteController(DeleteUseCase<Notes> deleteUseCase)
        {
            _deleteUseCase = deleteUseCase;
        }

        public async Task DeleteNote()
        {
            Console.Write("Введите ID заметки для удаления: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                await _deleteUseCase.DeleteAsync(id);
                Console.WriteLine("Заметка успешно удалена.");
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}
