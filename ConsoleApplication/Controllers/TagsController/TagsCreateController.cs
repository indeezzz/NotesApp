using Application.UseCases;
using Core.Models;

namespace ConsoleApplication.Controllers.TagsController
{
    public class TagsCreateController
    {
        private readonly CreateUseCase<Tags> _createUseCase;

        public TagsCreateController(CreateUseCase<Tags> createUseCase)
        {
            _createUseCase = createUseCase;
        }

        public async Task CreateTag()
        {
            Console.Write("Введите название тэга: ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.Write("Введите краткое описание тэга: ");
            var description = Console.ReadLine() ?? string.Empty;
          
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
            {
                var tag = new Tags
                {
                    Name = name,
                    Description = description
                };

                await _createUseCase.AddAsync(tag);
                Console.WriteLine("Тэг успешно создан.");
            }
            else
            {
                Console.WriteLine("Название и описание тэга не могут быть пустыми.");
            }
        }
    }
}
