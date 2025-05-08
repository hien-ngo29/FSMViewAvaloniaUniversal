using Avalonia;
using Avalonia.Data.Converters;

namespace FSMExpress.Controls.Sidebar;
public static class TypeIndentConverter
{
    private static readonly double INDENT_SIZE = 8.0;
    private static readonly double BASE_PADDING = 5.0;
    public static FuncValueConverter<int, Thickness> Converter { get; } =
        new FuncValueConverter<int, Thickness>(indent => new Thickness(
            INDENT_SIZE * indent + BASE_PADDING,
            BASE_PADDING,
            BASE_PADDING,
            BASE_PADDING
        ));
}
