using Application.UseCases;
using Core.Models;
using static MongoDB.Driver.WriteConcern;
using System.Collections;
using Infrastructure.Repositories;
using MongoDB.Driver;
namespace ConsoleApplication.Controllers.NotesController
{
    public class NotesCreateController
    {
        private readonly CreateUseCase<Notes> _createUseCase;
        private readonly MongoRepository<Tags> _tagsCollection;

        public NotesCreateController(CreateUseCase<Notes> createUseCase, MongoRepository<Tags> repository)
        {
            _createUseCase = createUseCase;
            _tagsCollection = repository;
        }

        public async Task CreateNote()
        {
            Console.Write("Введите название заметки: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите содержимое заметки: ");
            var body = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите путь к изображению: ");
            var imagePath = Console.ReadLine() ?? string.Empty;
            var imageBase64 = ImageToBase64(imagePath);

            Console.WriteLine("\nДоступные теги:");
            var allTags = await _tagsCollection.GetAllAsync();
            for (int i = 0; i < allTags.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {allTags.ElementAt(i).Name}");
            }

            Console.Write("Введите номера тегов через запятую (например, 1,2): ");
            var selectedTagIndices = Console.ReadLine()?.Split(',').Select(int.Parse).ToList();
            var selectedTagIds = selectedTagIndices?
                .Where(index => index > 0 && index <= allTags.Count())
                .Select(index => allTags.ElementAt(index - 1).Id)
                .ToList();

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(body))
            {
                var note = new Notes
                {
                    Name = name,
                    Body = body,
                    Image = imageBase64,
                    TagsId = selectedTagIds ?? new List<Guid>()
                };

                await _createUseCase.AddAsync(note);
                Console.WriteLine("Заметка успешно создана.");
            }
            else
            {
                Console.WriteLine("Название и текст заметки не могут быть пустыми.");
            }
        }

        private static string ImageToBase64(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                Console.WriteLine("Файл не найден.");
                return string.Empty;
            }

            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }
    }
}
