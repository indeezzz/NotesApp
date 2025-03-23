using ConsoleApplication.Controllers.TagsController;

namespace ConsoleApplication.Views
{
    public class TagsMenu : Menu
    {
        private static TagsCreateController? _tagsCreateController;
        private static TagsGetAllController? _tagsGetAllController;
        private static TagsGetByIdController? _tagsGetByIdController;
        private static TagsUpdateController? _tagsUpdateController;
        private static TagsDeleteController? _tagsDeleteController;
        public TagsMenu(
            TagsCreateController tagsCreateController,
            TagsGetAllController tagsGetAllController,
            TagsGetByIdController tagsGetByIdController,
            TagsUpdateController tagsUpdateController,
            TagsDeleteController tagsDeleteController) : base(new Dictionary<string, Func<Task>>
        {
            { "1", () => _tagsCreateController!.CreateTag()},
            { "2", () => _tagsGetAllController!.GetAllTags()},
            { "3", () => _tagsGetByIdController!.GetTagById() },
            { "4", () => _tagsUpdateController!.UpdateTag() },
            { "5", () => _tagsDeleteController!.DeleteTag() }
        }, new Dictionary<string, string>
        {
            { "1", "Создать тег" },
            { "2", "Список всех тегов" },
            { "3", "Получить тег по ID" },
            { "4", "Редактировать тег" },
            { "5", "Удалить тег" }
        })
        {
            _tagsCreateController = tagsCreateController;
            _tagsGetAllController = tagsGetAllController;
            _tagsGetByIdController = tagsGetByIdController;
            _tagsUpdateController = tagsUpdateController;
            _tagsDeleteController = tagsDeleteController;
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
