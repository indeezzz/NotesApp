using Core.Models;
using MongoDB.Driver;

namespace ConsoleApplication.Libs
{
    public class UpdateProperties<TModel> where TModel : class
    {
        protected UpdateProperties() 
        {

        }
        public static void UpdatePropertiesModel(TModel model, string[] excludedProperties, IEnumerable<Tags> allTags)
        {
            var properties = typeof(TModel).GetProperties();

            foreach (var property in properties)
            {
                if (excludedProperties.Contains(property.Name))
                {
                    continue; 
                }

                if (property.Name == nameof(Notes.TagsId) && property.PropertyType == typeof(List<Guid>))
                {
                    UpdateTags(model as Notes, allTags);
                    continue;
                }

                var currentValue = property.GetValue(model)?.ToString() ?? string.Empty;
                Console.Write($"Введите новое значение для {property.Name} (текущее: [{currentValue}]): ");
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    try
                    {
                        var newValue = Convert.ChangeType(input, property.PropertyType);
                        property.SetValue(model, newValue);
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
        public static void UpdatePropertiesModel(TModel model, string[] excludedProperties)
        {
            var properties = typeof(TModel).GetProperties();

            foreach (var property in properties)
            {
                if (excludedProperties.Contains(property.Name))
                {
                    continue;
                }

                var currentValue = property.GetValue(model)?.ToString() ?? string.Empty;
                Console.Write($"Введите новое значение для {property.Name} (текущее: [{currentValue}]): ");
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    try
                    {
                        var newValue = Convert.ChangeType(input, property.PropertyType);
                        property.SetValue(model, newValue);
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
        private static void UpdateTags(Notes? note, IEnumerable<Tags> allTags)
        {
            if (note == null || allTags == null || !allTags.Any())
            {
                Console.WriteLine("Теги недоступны для редактирования.");
                return;
            }

            // Вывод текущих тегов заметки
            var currentTags = allTags.Where(tag => note.TagsId.Contains(tag.Id)).Select(tag => tag.Name);
            Console.WriteLine("Текущие теги:");
            if (currentTags.Any())
            {
                Console.WriteLine(string.Join(", ", currentTags));
            }
            else
            {
                Console.WriteLine("Нет тегов.");
            }

            // Вывод доступных тегов
            Console.WriteLine("Доступные теги:");
            for (int i = 0; i < allTags.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {allTags.ElementAt(i).Name}");
            }

            // Выбор новых тегов
            Console.Write("Введите новые значения тегов через запятую (Например: 1,2): ");
            var selectedTagIndices = Console.ReadLine()?.Split(',').Select(s => int.TryParse(s.Trim(), out int index) ? index : -1).ToList();

            var selectedTagIds = selectedTagIndices?
                .Where(index => index > 0 && index <= allTags.Count())
                .Select(index => allTags.ElementAt(index - 1).Id)
                .ToList();

            if (selectedTagIds != null)
            {
                note.TagsId = selectedTagIds;
                Console.WriteLine("Теги успешно обновлены.");
            }
            else
            {
                Console.WriteLine("Ошибка при выборе тегов. Теги оставлены без изменений.");
            }
        }
    }
}
