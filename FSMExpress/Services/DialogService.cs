using Avalonia.Controls;
using System;
using System.Threading.Tasks;

namespace FSMExpress.Services;
public class DialogService(Window mainWindow, ViewLocator viewLocator) : IDialogService
{
    public async Task ShowDialog(IDialogAware viewModel)
    {
        var window = CreateWindow(viewModel);
        await window.ShowDialog(mainWindow);
    }

    public async Task<TResult?> ShowDialog<TResult>(IDialogAware<TResult> viewModel)
    {
        var window = CreateWindow(viewModel);

        void eventHandler(TResult? result) => window.Close(result);

        viewModel.RequestClose += eventHandler;
        await viewModel.AsyncInit();
        var result = await window.ShowDialog<TResult?>(mainWindow);
        viewModel.RequestClose -= eventHandler;

        return result;
    }

    private Window CreateWindow(IDialogAware viewModel)
    {
        var view = viewLocator.Build(viewModel);
        if (view is not UserControl uc)
        {
            throw new Exception("View is not a UserControl");
        }

        view.DataContext = viewModel;
        return new Window
        {
            Content = uc,
            Icon = mainWindow.Icon,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Title = viewModel.Title,
            Width = viewModel.Width,
            Height = viewModel.Height,
        };
    }
}
