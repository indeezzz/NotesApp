namespace Core.Interfaces
{
    public interface IView<in TModel>
    {
        void Display(TModel model);
    }
}
