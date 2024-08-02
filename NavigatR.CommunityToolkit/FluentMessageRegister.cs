using CommunityToolkit.Mvvm.Messaging;

namespace NavigatR.CommunityToolkit;

public sealed record BroadcastMessage<T> where T : class
{
    public T Recipient { get; init; }
    internal BroadcastMessage(T recipient)
    {
        Recipient = recipient;
    }

    public BroadcastMessage<T> On<TMessage>(MessageHandler<T, TMessage> messageHandler)
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

