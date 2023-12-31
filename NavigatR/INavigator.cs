﻿namespace NavigatR;

public interface INavigator
{
    void NavigateBackward(int? index = null);
    void NavigateBackwardTo<T>() where T : INavigable;
    void NavigateFoward(int? index = null);
    void NavigateFowardTo<T>() where T : INavigable;
    void NavigateTo<T>(object? parameter = null) where T : INavigable;
    void NavigateTo<T, TParam>(TParam parameter) where T : INavigable<TParam>;
}
