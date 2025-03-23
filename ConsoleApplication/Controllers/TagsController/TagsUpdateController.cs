using Application.UseCases;
using ConsoleApplication.Libs;
using Core.Models;

namespace ConsoleApplication.Controllers.TagsController
{
    public class TagsUpdateController
    {
        private readonly UpdateUseCase<Tags> _updateUseCase;
        private readonly GetByIdUseCase<Tags> _getByIdUseCase;
        public TagsUpdateController(UpdateUseCase<Tags> updateUseCase, GetByIdUseCase<Tags> getByIdUseCase)
        {
            _updateUseCase = updateUseCase;
            _getByIdUseCase = getByIdUseCase;
        }

        public async Task UpdateTag()
        {
            Console.Write("Введите ID тэга для обновления: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                var tag = await _getByIdUseCase.GetByIdAsync(id);
                if (tag != null)
                {
                    UpdateProperties<Tags>.UpdatePropertiesModel(tag, excludedProperties: [nameof(tag.Id)]);
                    await _updateUseCase.UpdateAsync(tag);
                    Console.WriteLine("Тэг успешно обновлен.");
                }
                else
                {
                    Console.WriteLine("Тэг не найден.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }
    }
}
