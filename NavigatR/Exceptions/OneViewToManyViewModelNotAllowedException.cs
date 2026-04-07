namespace NavigatR.Exceptions;

public class OneViewToManyViewModelNotAllowedException : Exception
{
    public OneViewToManyViewModelNotAllowedException(Type viewType, Type existingViewModel, Type newViewModel)
        : base($"The View '{viewType.Name}' is already mapped to ViewModel '{existingViewModel.Name}'. Cannot map it also to '{newViewModel.Name}'.")
    {
    }
}

