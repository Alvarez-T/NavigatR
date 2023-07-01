namespace NavigatR;

public interface INavigable 
{
    public Task Navigate()
    {
        throw new NotImplementedException("If you aren't using any wrapper, consider to implement the Navigate method");
    }
}

public interface INavigable<TParams> : INavigable
{
    TParams Parameter { get; }
}
