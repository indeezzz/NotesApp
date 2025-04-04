using ConsoleApplication.Controllers.TagsController;

namespace ConsoleApplication.Views
{
    public class TagsMenu : Menu
    {
        public TagsMenu(
            TagsCreateController tagsCreateController,
            TagsGetAllController tagsGetAllController,
            TagsGetByIdController tagsGetByIdController,
            TagsUpdateController tagsUpdateController,
            TagsDeleteController tagsDeleteController) : base(new Dictionary<string, Func<Task>>
        {
            { "1", () => tagsCreateController!.CreateTag()},
            { "2", () => tagsGetAllController!.GetAllTags()},
            { "3", () => tagsGetByIdController!.GetTagById() },
            { "4", () => tagsUpdateController!.UpdateTag() },
            { "5", () => tagsDeleteController!.DeleteTag() }
        }, new Dictionary<string, string>
        {
            { "1", "Создать тег" },
            { "2", "Список всех тегов" },
            { "3", "Получить тег по ID" },
            { "4", "Редактировать тег" },
            { "5", "Удалить тег" }
        })
        {

        }

        protected override void DisplayHeader()
        {
            Console.WriteLine("=== Управление тегами ===");
        }
        protected override void DisplayExit()
        {
            Console.WriteLine("Q. Назад");
        }
    }
}
