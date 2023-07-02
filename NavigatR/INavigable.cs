namespace NavigatR;

public interface INavigable 
{
    public Task Navigate()
    {
        throw new NotImplementedException("If you aren't using any navigation wrapper in your application configuration, you must declare the Navigate method in your implementation class");
    }
}

public interface INavigable<TParams> : INavigable
{
    TParams Parameter { get; }
}
