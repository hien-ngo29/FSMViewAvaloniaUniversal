using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FSMExpress.ViewModels.Dialogs;

namespace FSMExpress.Views.Dialogs;
public partial class SceneSelectorView : UserControl
{
    public SceneSelectorView()
    {
        InitializeComponent();
        Loaded += SceneSelectorView_Loaded;
    }

    private void SceneSelectorView_Loaded(object? sender, RoutedEventArgs e)
    {
        searchTextBox.Focus();
    }

    private void MainGrid_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            if (DataContext is SceneSelectorViewModel vm)
            {
                vm.PickSelectedEntry();
            }
        }
        else if (e.Key == Key.Escape)
        {
            e.Handled = true;
            if (DataContext is SceneSelectorViewModel vm)
            {
                vm.PickCancel();
            }
        }
    }

    public void ListBoxItem_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is SceneSelectorViewModel vm)
        {
            vm.PickSelectedEntry();
        }
    }
}
