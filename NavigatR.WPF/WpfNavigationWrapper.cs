namespace NavigatR.WPF
{
    public class WpfNavigationWrapper<T> : INavigationWrapper<T>
        where T : INavigable
    {
        public Task ExecuteNavigation(INavigable navigable)
        {
            return Task.CompletedTask;
        }
    }
}