using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FSMExpress.ViewModels.Dialogs;

namespace FSMExpress.Views.Dialogs;
public partial class FsmSelectorView : UserControl
{
    public FsmSelectorView()
    {
        InitializeComponent();
        Loaded += FsmSelectorView_Loaded;
    }

    private void FsmSelectorView_Loaded(object? sender, RoutedEventArgs e)
    {
        searchTextBox.Focus();
    }

    private void MainGrid_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            if (DataContext is FsmSelectorViewModel vm)
            {
                vm.PickSelectedEntry();
            }
        }
        else if (e.Key == Key.Escape)
        {
            e.Handled = true;
            if (DataContext is FsmSelectorViewModel vm)
            {
                vm.PickCancel();
            }
        }
    }

    public void ListBoxItem_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is FsmSelectorViewModel vm)
        {
            vm.PickSelectedEntry();
        }
    }
}
