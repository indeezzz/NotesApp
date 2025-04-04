using ConsoleApplication.Controllers.NotesController;

namespace ConsoleApplication.Views
{
    public class MainMenu : Menu
    {
        public MainMenu(NotesCreateController notesCreateController,
            NotesGetAllController notesGetAllController,
            NotesGetByIdController notesGetByIdController,
            NotesUpdateController notesUpdateController,
            NotesDeleteController notesDeleteController,
            TagsMenu tagsMenu) : base(new Dictionary<string, Func<Task>>
            {
                { "1", () => notesCreateController!.CreateNote() },
                { "2", () => notesGetAllController!.GetAllNotes() },
                { "3", () => notesGetByIdController!.GetNoteById() },
                { "4", () => notesUpdateController!.UpdateNote() },
                { "5", () => notesDeleteController!.DeleteNote() },
                { "6", () => tagsMenu!.ShowAsync()}
            }, new Dictionary<string, string>
            {
                { "1", "Создать заметку" },
                { "2", "Список всех заметок" },
                { "3", "Найти заметку по ID" },
                { "4", "Редактировать заметку"},
                { "5", "Удалить заметку" },
                { "6", "Меню управления тэгами"}
            })
        {

        }

        protected override void DisplayHeader()
        {
            Console.WriteLine("=== Главное меню ===");
        }
    }
}
