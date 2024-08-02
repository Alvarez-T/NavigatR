using NavigatR;
using NavigatR.Providers;

namespace CommunityToolkit.Mvvm.Messaging;

public static class MessengerExtensions
{
    public static void Register<TRecipient, TMessage>(this IMessenger messenger, MessageHandler<TRecipient, TMessage> handler)
        where TRecipient : class, IViewModel
        where TMessage : class
    {
        var viewModel = ViewModelLocator.LocateViewModel<TRecipient>();
        messenger.Register(viewModel, handler);
    }

    public static void Register<TRecipient, TMessage>(this IMessenger messenger, IViewModelProvider viewModelProvider, MessageHandler<TRecipient, TMessage> handler)
        where TRecipient : class, IViewModel
        where TMessage : class
    {
        var viewModel = viewModelProvider.GetViewModel<TRecipient>();
        messenger.Register(viewModel, handler);
    }
}