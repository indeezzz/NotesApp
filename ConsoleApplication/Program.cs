using Application.UseCases;
using Core.Models;
using Infrastructure.Repositories;
using System;

namespace Program;

class Program
{
    private readonly CreateNoteUseCase _createNoteUseCase;
    private readonly GetAllNotesUseCase _getAllNotesUseCase;
    private readonly GetNoteByIdUseCase _getNoteByIdUseCase;
    private readonly UpdateNoteUseCase _updateNoteUseCase;
    private readonly DeleteNoteUseCase _deleteNoteUseCase;
    private static readonly string connectionString = "mongodb://127.0.0.1:27017";
    private static readonly string databaseName = "NotesDatabase";
    private static readonly string collectionName = "Notes";

    public Program(
        CreateNoteUseCase createNoteUseCase,
        GetAllNotesUseCase getAllNotesUseCase,
        GetNoteByIdUseCase getNoteByIdUseCase,
        UpdateNoteUseCase updateNoteUseCase,
        DeleteNoteUseCase deleteNoteUseCase)
    {
        _createNoteUseCase = createNoteUseCase;
        _getAllNotesUseCase = getAllNotesUseCase;
        _getNoteByIdUseCase = getNoteByIdUseCase;
        _updateNoteUseCase = updateNoteUseCase;
        _deleteNoteUseCase = deleteNoteUseCase;
    }

    static async Task Main(string[] args)
    {
        try
        {
            var repository = new MongoNotesRepository(connectionString, databaseName, collectionName);

            var app = new Program(
                new CreateNoteUseCase(repository),
                new GetAllNotesUseCase(repository),
                new GetNoteByIdUseCase(repository),
                new UpdateNoteUseCase(repository),
                new DeleteNoteUseCase(repository));

            await app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private async Task Run()
    {
            while (true)
            {
                Console.Clear();
                await GetAllNotes();
                Console.WriteLine("1. Добавить заметку");
                Console.WriteLine("2. Вывести заметку по ID");
                Console.WriteLine("3. Редактировать заметку");
                Console.WriteLine("4. Удалить заметку");
                Console.WriteLine("Q. Выйти из программы");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine()?.Trim().ToLower();

                switch (choice)
                {
                    case "1":
                        await CreateNote();
                        break;
                    case "2":
                        await GetNoteById();
                        break;
                    case "3":
                        await UpdateNote();
                        break;
                    case "4":
                        await DeleteNote();
                        break;
                    case "q":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
    }

    private async Task CreateNote()
    {
        Console.Write("Введите название заметки: ");
        string name = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Введите содержимое заметки: ");
        string body = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Write("Введите путь к изображению: ");
        string imagePath = Console.ReadLine() ?? string.Empty;

        string base64Image = ImageToBase64(imagePath);

        if (string.IsNullOrEmpty(base64Image))
        {
            Console.WriteLine("Изображение не было загружено.");
            return;
        }

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(body))
        {
            await _createNoteUseCase.ExecuteAsync(name, body, base64Image);
            Console.WriteLine("Заметка успешно создана.");
        }
        else
        {
            Console.WriteLine("Название и текст заметки не могут быть пустыми.");
        }
    }

    private async Task GetAllNotes()
    {
        var notes = await _getAllNotesUseCase.ExecuteAsync();
        if (notes.Any())
        {
            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id}\nДата создания: {note.CreatedDateTime}\nЗаголовок: {note.Name}\nТекст: {note.Body}\nКартинка: {note.Image}");
            }
        }
        else
        {
            Console.WriteLine("Нет заметок.");
        }
    }

    private async Task GetNoteById()
    {
        Console.Write("Введите ID заметки: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var note = await _getNoteByIdUseCase.ExecuteAsync(id);
            if (note != null)
            {
                Console.WriteLine($"ID: {note.Id}, Дата создания: {note.CreatedDateTime}, Заголовок: {note.Name}, Текст: {note.Body}, Картинка: {note.Image}");
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

    private async Task UpdateNote()
    {
        Console.Write("Введите ID заметки для обновления: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var note = await _getNoteByIdUseCase.ExecuteAsync(id);
            if (note != null)
            {
                UpdateProperties(note, excludedProperties: new[] { nameof(note.Id), nameof(note.CreatedDateTime) });
                await _updateNoteUseCase.ExecuteAsync(note);
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

    private void UpdateProperties(Notes note, string[] excludedProperties)
    {
        var properties = typeof(Notes).GetProperties();

        foreach (var property in properties)
        {
            if (excludedProperties.Contains(property.Name))
            {
                continue;
            }

            var currentValue = property.GetValue(note)?.ToString() ?? string.Empty;
            Console.Write($"Введите новое значение для {property.Name} (текущее: [{currentValue}]): ");
            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    var newValue = Convert.ChangeType(input, property.PropertyType);
                    property.SetValue(note, newValue);
                    Console.WriteLine($"{property.Name} успешно обновлено.");
                }
                catch
                {
                    Console.WriteLine($"Ошибка: неверный формат для {property.Name}. Значение оставлено без изменений.");
                }
            }
            else
            {
                Console.WriteLine($"{property.Name} оставлено без изменений.");
            }
        }
    }


    private async Task DeleteNote()
    {
        Console.Write("Введите ID заметки для удаления: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            await _deleteNoteUseCase.ExecuteAsync(id);
            Console.WriteLine("Заметка успешно удалена.");
        }
        else
        {
            Console.WriteLine("Неверный формат ID.");
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