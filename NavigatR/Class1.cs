namespace NavigatR;

internal sealed class NavigationService
{

}

public class Model
{
    public int Code { get; set; }
    public string Name { get; set; }
}


public class ViewModel : INavigable
{
    private readonly INavigator _navigator;

    public ViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }

    public async Task FooCommand()
    {
        await _navigator.NavigateTo<OtherViewModel>();
        await _navigator.NavigateTo<FooViewModel, Model?>(new Model());

    }
}

public class OtherViewModel : INavigable
{

}

public class FooViewModel : INavigable<Model?>
{
    public Model? Parameter { get; }

}
