namespace NavigatR.Exceptions;

public class NavigatRNotConfiguredException : Exception
{
    public NavigatRNotConfiguredException() 
        : base("The NavigatR library was not configured. Ensure that the method \"NavigatRExtensions.UseNavigatR()\" was called into your IoC Container")
    {

    }
}