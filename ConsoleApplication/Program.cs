using Application.UseCases;
using ConsoleApplication.Controllers.NotesController;
using ConsoleApplication.Controllers.TagsController;
using ConsoleApplication.Views;
using Core.Models;
using Infrastructure.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Program;

class Program
{
    private static readonly string connectionString = "mongodb://localhost:27017";
    private static readonly string databaseName = "NotesDatabase";

    protected Program()
    {

    }

    static async Task Main(string[] args)
    {
        try
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            var tagsRepository = new MongoRepository<Tags>(connectionString, databaseName, "Tags");
            var notesRepository = new MongoRepository<Notes>(connectionString, databaseName, "Notes");

            var tagsMenu = new TagsMenu(
                new TagsCreateController(new CreateUseCase<Tags>(tagsRepository)), 
                new TagsGetAllController(new GetAllUseCase<Tags>(tagsRepository)), 
                new TagsGetByIdController(new GetByIdUseCase<Tags>(tagsRepository)), 
                new TagsUpdateController(new UpdateUseCase<Tags>(tagsRepository), new GetByIdUseCase<Tags>(tagsRepository)), 
                new TagsDeleteController(new DeleteUseCase<Tags>(tagsRepository), notesRepository));
            var mainMenu = new MainMenu(
                new NotesCreateController(new CreateUseCase<Notes>(notesRepository), tagsRepository),
                new NotesGetAllController(new GetAllUseCase<Notes>(notesRepository), tagsRepository),
                new NotesGetByIdController(new GetByIdUseCase<Notes>(notesRepository), tagsRepository),
                new NotesUpdateController(new UpdateUseCase<Notes>(notesRepository), new GetByIdUseCase<Notes>(notesRepository), tagsRepository),
                new NotesDeleteController(new DeleteUseCase<Notes>(notesRepository)),                           
                tagsMenu);

            await mainMenu.ShowAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }   
}