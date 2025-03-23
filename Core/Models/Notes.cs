namespace Core.Models
{
    public class Notes
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        // Список ID тегов, связанных с заметкой
        public List<Guid> TagsId { get; set; } = new List<Guid>();

        public override string ToString()
        {
            var tagString = TagsId.Any() ? string.Join(", ", TagsId) : "Нет тегов";
            return $"ID: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Содержимое заметки: {Body}\n" +
                   $"Картинка в формате Base64: {Image}\n" +
                   $"Тэги: {tagString}\n";
        }

        // Метод для вывода имен тегов
        public string ToStringWithTagNames(IEnumerable<Tags> allTags)
        {
            var tagNames = allTags
                .Where(tag => TagsId.Contains(tag.Id))
                .Select(tag => tag.Name);

            var tagString = tagNames.Any() ? string.Join(", ", tagNames) : "Нет тегов";

            return $"ID: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Содержимое заметки: {Body}\n" +
                   $"Картинка в формате Base64: {Image}\n" +
                   $"Тэги: {tagString}\n";
        }
    }
}
