using Avalonia.Controls;
using Avalonia.Input;
using FSMExpress.ViewModels.Dialogs;

namespace FSMExpress.Views.Dialogs;
public partial class FsmSelectorView : UserControl
{
    public FsmSelectorView()
    {
        InitializeComponent();
    }

    public void ListBoxItem_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is FsmSelectorViewModel vm)
        {
            vm.ListBoxItem_DoubleTapped();
        }
    }
}
