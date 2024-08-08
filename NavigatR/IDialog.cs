namespace NavigatR;

public interface IDialog
{
    public void Show(object view);
    public void Show<T>();
    public void Close();
}

public static class DialogExtensions
{
    public static TResult ShowByViewModel<TResult>(this IDialog dialog, IViewModel viewModel)
        => throw new NotImplementedException();

    public static TResult ShowByViewModel<TViewModel, TResult>(this IDialog dialog)
        => throw new NotImplementedException();

    public static TResult ShowByView<TView, TResult>(this IDialog dialog)
        => throw new NotImplementedException();
}

public interface IMessageBox
{
    public void ShowMessage(string title, string message, MessageType type, MessageIcon icon);
    public bool ShowQuestion(string title, string question, MessageType type = MessageType.YesNo, MessageIcon icon = MessageIcon.Question);
    public void ShowMessage<TCustom>(string title, string message);
}

public static class MessageBoxExtensions
{
    public static void ShowInfo(this IMessageBox messageBox, string title, string message, MessageType type = MessageType.Ok)
        => messageBox.ShowMessage(title, message, type, MessageIcon.Info);

    public static void ShowAlert(this IMessageBox messageBox, string title, string message, MessageType type = MessageType.Ok)
        => messageBox.ShowMessage(title, message, type, MessageIcon.Warning);

    public static void ShowError(this IMessageBox messageBox, string title, string message, MessageType type = MessageType.Ok)
        => messageBox.ShowMessage(title, message, type, MessageIcon.Error);

    public static void ShowSuccess(this IMessageBox messageBox, string title, string message, MessageType type = MessageType.Ok)
        => messageBox.ShowMessage(title, message, type, MessageIcon.Success);

    public static void ShowMessage(this IMessageBox messageBox, string title, string message, MessageType type = MessageType.Ok)
        => messageBox.ShowMessage(title, message, type, MessageIcon.None);

}

public enum MessageType
{
    Custom,
    Ok,
    OkCancel,
    YesNo,
    YesNoCancel
}

public enum MessageIcon
{
    None,
    Success,
    Info,
    Error,
    Warning,
    Question
}

