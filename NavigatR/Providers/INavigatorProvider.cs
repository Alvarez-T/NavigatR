namespace NavigatR.Providers;

public interface INavigatorProvider
{
    public TNavigator GetNavigator<TNavigator>() where TNavigator : class, INavigator;
    public INavigator GetDefaultNavigator();
}