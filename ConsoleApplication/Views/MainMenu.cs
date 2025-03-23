using ConsoleApplication.Controllers;
using ConsoleApplication.Controllers.NotesController;

namespace ConsoleApplication.Views
{
    public class MainMenu : Menu
    {
        private static NotesCreateController? _notesCreateController;
        private static NotesGetAllController? _notesGetAllController;
        private static NotesGetByIdController? _notesGetByIdController;
        private static NotesUpdateController? _notesUpdateController;
        private static NotesDeleteController? _notesDeleteController;
        private static TagsMenu? _tagsMenu;


        public MainMenu(NotesCreateController notesCreateController,
            NotesGetAllController notesGetAllController,
            NotesGetByIdController notesGetByIdController,
            NotesUpdateController notesUpdateController,
            NotesDeleteController notesDeleteController,
            TagsMenu tagsMenu): base(new Dictionary<string, Func<Task>>
            {
                { "1", () => _notesCreateController!.CreateNote() },
                { "2", () => _notesGetAllController!.GetAllNotes() },
                { "3", () => _notesGetByIdController!.GetNoteById() },
                { "4", () => _notesUpdateController!.UpdateNote() },
                { "5", () => _notesDeleteController!.DeleteNote() },
                { "6", () => _tagsMenu!.ShowAsync()}
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
                _notesCreateController = notesCreateController;
                _notesGetAllController = notesGetAllController;
                _notesGetByIdController = notesGetByIdController;
                _notesUpdateController = notesUpdateController;
                _notesDeleteController = notesDeleteController;
                _tagsMenu = tagsMenu;
            }

        protected override void DisplayHeader()
        {
            Console.WriteLine("=== Главное меню ===");
        }
    }
}
