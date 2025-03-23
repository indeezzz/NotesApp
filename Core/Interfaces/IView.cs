namespace Core.Interfaces
{
    public interface IView<TModel>
    {
        void Display(TModel model);
    }
}
