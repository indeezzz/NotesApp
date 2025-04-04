
namespace Core.Interfaces
{
    public interface ICreateUseCase<in TModel> where TModel : class
    {
        Task AddAsync(TModel model);
    }
}
