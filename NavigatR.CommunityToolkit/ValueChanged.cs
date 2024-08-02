namespace CommunityToolkit.Mvvm.Messaging.Messages;

public sealed record ValueChanged<T>(T Value)
{
    public static implicit operator T(ValueChanged<T> valueChanged) => valueChanged.Value;
}