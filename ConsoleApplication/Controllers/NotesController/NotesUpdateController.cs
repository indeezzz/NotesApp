using Application.UseCases;
using ConsoleApplication.Libs;
using Core.Models;
using Infrastructure.Repositories;

namespace ConsoleApplication.Controllers.NotesController
{
    public class NotesUpdateController
    {
        private readonly UpdateUseCase<Notes> _updateUseCase;
        private readonly GetByIdUseCase<Notes> _getByIdUseCase;
        private readonly MongoRepository<Tags> _tagsCollection;
        public NotesUpdateController(UpdateUseCase<Notes> updateUseCase, GetByIdUseCase<Notes> getByIdUseCase, MongoRepository<Tags> repository)
        {
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
            _tagsCollection = repository;
        }

        public async Task UpdateNote()
        {
            Console.Write("Введите ID заметки для обновления: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                var note = await _getByIdUseCase.GetByIdAsync(id);
                var tags = await _tagsCollection.GetAllAsync();

                if (note != null)
                {
                    UpdateProperties<Notes>.UpdatePropertiesModel(note, excludedProperties: [nameof(note.Id), nameof(note.CreatedDateTime)], tags);
                    await _updateUseCase.UpdateAsync(note);
                    Console.WriteLine("Заметка успешно обновлена.");
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
