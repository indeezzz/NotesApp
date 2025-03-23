using Application.UseCases;
using Core.Models;

namespace ConsoleApplication.Controllers.TagsController
{
    public class TagsGetByIdController
    {
        private readonly GetByIdUseCase<Tags> _getByIdUseCase;

        public TagsGetByIdController(GetByIdUseCase<Tags> getByIdUseCase)
        {
            _getByIdUseCase = getByIdUseCase;
        }

        public async Task GetTagById()
        {
            Console.Write("Введите ID тэга: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                var tag = await _getByIdUseCase.GetByIdAsync(id);
                if (tag != null)
                {
                    tag.ToString();
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
