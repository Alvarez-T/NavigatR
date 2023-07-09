namespace NavigatR;

public interface INavigationContainer
{
    Guid Id => Guid.NewGuid();
    void ShowView(object view);
}

