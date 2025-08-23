using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FSMExpress.Controls.Sidebar;
public class BoolObjectConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values == null || values.Count < 3)
            return null;

        if (values[0] is bool selector)
        {
            int selectedIndex = selector ? 1 : 2;
            if (selectedIndex < values.Count)
            {
                return values[selectedIndex];
            }
        }

        // something went wrong, give up
        return values.Skip(1).FirstOrDefault();
    }
}