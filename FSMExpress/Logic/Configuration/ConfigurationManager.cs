using System;
using System.IO;
using System.Text.Json;

namespace FSMExpress.Logic.Configuration;
public static class ConfigurationManager
{
    public const string CONFIG_FILENAME = "config.json";
    public static ConfigurationValues Settings { get; }
    public static bool IsInitialized { get; } = false;

    static ConfigurationManager()
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILENAME);
        if (!File.Exists(configPath))
        {
            Settings = new ConfigurationValues();
            IsInitialized = true;
        }
        else
        {
            var configText = File.ReadAllText(configPath);
            Settings = JsonSerializer.Deserialize<ConfigurationValues>(configText) ?? new ConfigurationValues();
            IsInitialized = true;
        }
    }

    public static void SaveConfig()
    {
        if (!IsInitialized)
            return;

        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILENAME);
        var configText = JsonSerializer.Serialize(Settings);
        File.WriteAllText(configPath, configText);
    }
}
