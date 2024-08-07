using CommunityToolkit.Mvvm.Messaging;

namespace NavigatR.CommunityToolkit;

public class BroadcastMessage<T> where T : class
{
    public T Recipient { get; init; }
    protected internal BroadcastMessage(T recipient)
    {
        Recipient = recipient;
    }

    public BroadcastMessage<T> OnViewMessage<TMessage>(MessageHandler<T, TMessage> messageHandler)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Register(Recipient, messageHandler);
        return this;
    }
}

public static class BroadcastMessage
{
    public static BroadcastMessage<T> To<T>(T recipient)
        where T : class => new(recipient);
}

