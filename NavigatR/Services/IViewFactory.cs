namespace NavigatR.Services;

public interface IViewFactory
{
    object CreateView(Type type);
}

