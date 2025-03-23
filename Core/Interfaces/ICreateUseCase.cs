
namespace Core.Interfaces
{
    public interface ICreateUseCase<TModel> where TModel : class
    {
        Task AddAsync(TModel model);
    }
}
