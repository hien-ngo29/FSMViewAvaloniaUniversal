using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using FSMExpress.Common.Document;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FSMExpress.Controls.Sidebar;
public class TypeColorConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Count != 2 || !targetType.IsAssignableFrom(typeof(IBrush)))
            throw new NotSupportedException();

        if (parameter is not string paramType)
            throw new NotSupportedException();

        if (values[0] is not FsmDocumentNodeDataFieldKind kind)
            return BindingOperations.DoNothing;

        if (paramType == "Type")
        {
            // type name color
            return kind switch
            {
                FsmDocumentNodeDataFieldKind.Boolean => GetBrushFromName("TypeTextPrimitive"),
                FsmDocumentNodeDataFieldKind.Integer => GetBrushFromName("TypeTextPrimitive"),
                FsmDocumentNodeDataFieldKind.Float => GetBrushFromName("TypeTextPrimitive"),
                FsmDocumentNodeDataFieldKind.String => GetBrushFromName("TypeTextPrimitive"),
                FsmDocumentNodeDataFieldKind.Array => GetBrushFromName("TypeTextType"),
                FsmDocumentNodeDataFieldKind.Object => GetBrushFromName("TypeTextType"),
                _ => GetBrushFromName("TypeTextType")
            };
        }
        else if (paramType == "Value")
        {
            // value color
            return kind switch
            {
                FsmDocumentNodeDataFieldKind.Boolean => GetBrushFromName("TypeTextPrimitive"),
                FsmDocumentNodeDataFieldKind.Integer => GetBrushFromName("TypeTextValue"),
                FsmDocumentNodeDataFieldKind.Float => GetBrushFromName("TypeTextValue"),
                FsmDocumentNodeDataFieldKind.String => GetBrushFromName("TypeTextString"),
                FsmDocumentNodeDataFieldKind.Array => GetBrushFromName("TypeTextPlain"),
                FsmDocumentNodeDataFieldKind.Object => GetBrushFromName("TypeTextPlain"),
                _ => GetBrushFromName("TypeTextPlain")
            };
        }

        return BindingOperations.DoNothing;
    }

    private static ISolidColorBrush GetBrushFromName(string key)
    {
        var currentApp = Application.Current;
        if (currentApp is null)
            return new SolidColorBrush(Colors.Black);

        if (!currentApp.TryFindResource(key, currentApp.ActualThemeVariant, out object? value))
            return new SolidColorBrush(Colors.Black);

        if (value is not ISolidColorBrush brush)
            return new SolidColorBrush(Colors.Black);

        return brush;
    }
}