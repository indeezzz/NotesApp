namespace Core.Interfaces
{
    public interface IUpdateUseCase<in TModel> where TModel : class
    {
        Task UpdateAsync(TModel model);
    }
}
