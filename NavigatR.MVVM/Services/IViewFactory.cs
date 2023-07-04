namespace NavigatR.MVVM.Services;

public interface IViewFactory
{
    object CreateView(Type type);
}

