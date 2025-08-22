using System.ComponentModel;

namespace FSMExpress.Logic.Configuration;
public class ConfigurationValues : INotifyPropertyChanged
{
    private ConfigurationThemeType _themeType = ConfigurationThemeType.Auto;
    public ConfigurationThemeType ThemeType
    {
        get => _themeType;
        set
        {
            _themeType = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThemeType)));
            ConfigurationManager.SaveConfig();
        }
    }

    private string? _defaultGamePath = null;
    public string? DefaultGamePath
    {
        get => _defaultGamePath;
        set
        {
            _defaultGamePath = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DefaultGamePath)));
            ConfigurationManager.SaveConfig();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

public enum ConfigurationThemeType
{
    Auto,
    Light,
    Dark
}