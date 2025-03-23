using Application.UseCases;
using Core.Models;

namespace ConsoleApplication.Controllers.TagsController
{
    public class TagsGetAllController
    {
        private readonly GetAllUseCase<Tags> _getAllUseCase;

        public TagsGetAllController(GetAllUseCase<Tags> getAllUseCase)
        {
            _getAllUseCase = getAllUseCase;
        }

        public async Task GetAllTags()
        {
            var tags = await _getAllUseCase.GetAllAsync();
            if (tags.Any())
            {
                foreach (var tag in tags)
                {
                    Console.WriteLine($"ID: {tag.Id}\nНазвание тэга: {tag.Name}\nОписание тэга: {tag.Description}");
                }
            }
            else
            {
                Console.WriteLine("Нет тэгов.");
            }
        }

    }
}
