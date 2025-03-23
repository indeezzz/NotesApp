using Application.UseCases;
using Core.Models;
using Infrastructure.Repositories;

namespace ConsoleApplication.Controllers.NotesController
{
    public class NotesGetByIdController
    {
        private readonly GetByIdUseCase<Notes> _getByIdUseCase;
        private readonly MongoRepository<Tags> _tagsCollection;

        public NotesGetByIdController(GetByIdUseCase<Notes> getByIdUseCase, MongoRepository<Tags> repository)      
        {
            _getByIdUseCase = getByIdUseCase;
            _tagsCollection = repository;
        }

        public async Task GetNoteById()
        {
            Console.Write("Введите ID заметки: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                var note = await _getByIdUseCase.GetByIdAsync(id);
                var tags = await _tagsCollection.GetAllAsync();
                if (note != null)
                {
                    Console.WriteLine(note.ToStringWithTagNames(tags));
                }
                else
                {
                    Console.WriteLine("Заметка не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}
