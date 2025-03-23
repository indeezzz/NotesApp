using Application.UseCases;
using Core.Models;
using Infrastructure.Repositories;

namespace ConsoleApplication.Controllers.NotesController
{
    public class NotesGetAllController
    {
        private readonly GetAllUseCase<Notes> _getAllUseCase;
        private readonly MongoRepository<Tags> _tagsCollection;

        public NotesGetAllController(GetAllUseCase<Notes> getAllUseCase, MongoRepository<Tags> repository)
        {
            _getAllUseCase = getAllUseCase;
            _tagsCollection = repository;
        }

        public async Task GetAllNotes()
        {
            var notes = await _getAllUseCase.GetAllAsync();
            var tags = await _tagsCollection.GetAllAsync();
            if (notes.Any())
            {

                foreach (var note in notes)
                {
                    Console.WriteLine(note.ToStringWithTagNames(tags));
                }
            }
            else
            {
                Console.WriteLine("Нет заметок.");
            }
        }
    }
}
