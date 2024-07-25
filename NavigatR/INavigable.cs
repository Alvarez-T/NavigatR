namespace NavigatR;

public interface INavigable 
{
    public Task<bool> CanNavigate()
        => Task.FromResult(true);

    public Task OnNavigation()
        => Task.CompletedTask;
}
