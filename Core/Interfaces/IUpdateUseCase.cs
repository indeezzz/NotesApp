namespace Core.Interfaces
{
    public interface IUpdateUseCase<TModel> where TModel : class
    {
        Task UpdateAsync(TModel model);
    }
}
