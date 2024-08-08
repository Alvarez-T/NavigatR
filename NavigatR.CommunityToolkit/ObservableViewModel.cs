using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using NavigatR.Providers;

namespace NavigatR.CommunityToolkit;

public abstract class ObservableViewModel : ObservableObject, IViewModel
{
    protected IViewModelProvider ViewModelProvider { get; }
    protected IMessageBox MessageBox { get; }
    protected IDialog Dialog { get; }

    public ObservableViewModel()
    {
        //TODO: Default MsgBox and Dialog
    }

    protected void SetPropertyAndBroadcast<T>(ref T field, T value, string? propertyName = null)
    {
        this.SetProperty<T>(ref field, value, propertyName);
    }

    protected virtual void Broadcast<T>(T newValue)
    {
        WeakReferenceMessenger.Default.Send(new ValueChanged<T>(newValue));
    }
    
}

public abstract class NavigableViewModel : ObservableViewModel, INavigableViewModel
{
    protected INavigator Navigator { get; }
}