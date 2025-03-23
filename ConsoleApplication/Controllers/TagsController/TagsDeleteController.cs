using Application.UseCases;
using Core.Models;
using Infrastructure.Repositories;

namespace ConsoleApplication.Controllers.TagsController
{
    public class TagsDeleteController
    {
        private readonly DeleteUseCase<Tags> _deleteUseCase;
        private readonly MongoRepository<Notes> _notesCollection;

        public TagsDeleteController(DeleteUseCase<Tags> deleteUseCase, MongoRepository<Notes> repository)
        {
            _deleteUseCase = deleteUseCase;
            _notesCollection = repository;
        }

        public async Task DeleteTag()
        {
            Console.Write("Введите ID тэга для удаления: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                await _deleteUseCase.DeleteAsync(id);
                await _notesCollection.UpdateManyAsync("TagsId", id);
                Console.WriteLine("Тэг успешно удален.");
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}
