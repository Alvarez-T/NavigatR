namespace NavigatR;

public interface INavigable : IViewModel
{
    Task<bool> CanNavigate();
    Task OnNavigation();
}
