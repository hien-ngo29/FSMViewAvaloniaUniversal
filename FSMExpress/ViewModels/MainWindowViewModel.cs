using AssetsTools.NET.Extra;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using FSMExpress.Common.Assets;
using FSMExpress.Common.Document;
using FSMExpress.Logic.Configuration;
using FSMExpress.Logic.Util;
using FSMExpress.PlayMaker;
using FSMExpress.Services;
using FSMExpress.ViewModels.Dialogs;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace FSMExpress.ViewModels;
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly AssetsManager _manager = new();

    [ObservableProperty]
    private string? _lastOpenedPath = null;

    [ObservableProperty]
    private FsmDocument? _activeDocument = null;

    [ObservableProperty]
    private ObservableCollection<FsmDocument> _documents = [];

    [ObservableProperty]
    public FsmDocumentNode? _selectedNode = null;

    public MainWindowViewModel()
    {
        _manager.UseMonoTemplateFieldCache = true;
        _manager.UseTemplateFieldCache = true;
        _manager.UseQuickLookup = true;
    }

    public async Task<string?> PickScene(string ggmPath)
    {
        var dialogService = Ioc.Default.GetRequiredService<IDialogService>();

        var fileInst = _manager.LoadAssetsFile(ggmPath);
        if (!_manager.LoadClassDatabase(fileInst))
        {
            await MessageBoxUtil.ShowDialog("Class database failed to load", "Couldn't load class database class. Check if classdata.tpk exists?");
            return null;
        }

        var sceneChoice = await dialogService.ShowDialog(new SceneSelectorViewModel(_manager, fileInst));
        if (sceneChoice == null)
            return null;

        return Path.Combine(Path.GetDirectoryName(ggmPath)!, sceneChoice.FileName);
    }

    public async Task<AssetExternal?> PickFsm(string filePath)
    {
        var dialogService = Ioc.Default.GetRequiredService<IDialogService>();

        var fileType = FileTypeDetector.DetectFileType(filePath);
        if (fileType == DetectedFileType.BundleFile)
        {
            await MessageBoxUtil.ShowDialog("Unsupported type", "Sorry, bundles aren't supported yet.");
            return null;
        }
        else if (fileType == DetectedFileType.Unknown)
        {
            await MessageBoxUtil.ShowDialog("Unsupported type", "Could not detect this as a valid Unity file.");
            return null;
        }

        var fileInst = _manager.LoadAssetsFile(filePath);
        if (!_manager.LoadClassDatabase(fileInst))
        {
            await MessageBoxUtil.ShowDialog("Class database failed to load", "Couldn't load class database class. Check if classdata.tpk exists?");
            return null;
        }

        var fsmChoice = await dialogService.ShowDialog(new FsmSelectorViewModel(_manager, fileInst));
        if (fsmChoice == null)
            return null;

        var fsmFileInst = _manager.FileLookup[fsmChoice.Ptr.FilePath];
        return _manager.GetExtAsset(fsmFileInst, 0, fsmChoice.Ptr.PathId);
    }

    private void LoadPlaymakerFsm(AssetExternal fsmExt)
    {
        var fsmBaseField = fsmExt.baseField;
        var fsmFileInst = fsmExt.file;

        var fsmObject = new FsmPlaymaker(new AfAssetField(fsmBaseField["fsm"], new AfAssetNamer(_manager, fsmFileInst)));
        var fsmDoc = fsmObject.MakeDocument();
        Documents.Add(fsmDoc);
        ActiveDocument = fsmDoc;
    }

    public async void FileOpen()
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
            return;

        var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open a file",
            FileTypeFilter = [StorageService.Any],
        });

        var fileNames = FileDialogUtils.GetOpenFileDialogFiles(result);
        if (fileNames.Length == 0)
            return;

        var fileName = fileNames[0];
        LastOpenedPath = fileName;

        var selectedFsm = await PickFsm(fileName);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public async void FileOpenSceneList()
    {
        string? ggmPath;
        if (ConfigurationManager.Settings.DefaultGamePath is not null)
            ggmPath = Path.Combine(ConfigurationManager.Settings.DefaultGamePath, "globalgamemanagers");
        else
            ggmPath = await PickGamePathWithFile("globalgamemanagers");

        if (ggmPath is null)
            return;

        var scenePath = await PickScene(ggmPath);
        if (string.IsNullOrEmpty(scenePath))
            return;

        LastOpenedPath = scenePath;

        var selectedFsm = await PickFsm(scenePath);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public async void FileOpenResourcesAssets()
    {
        string? resourcesPath;
        if (ConfigurationManager.Settings.DefaultGamePath is not null)
            resourcesPath = Path.Combine(ConfigurationManager.Settings.DefaultGamePath, "resources.assets");
        else
            resourcesPath = await PickGamePathWithFile("resources.assets");

        if (resourcesPath is null)
            return;

        LastOpenedPath = resourcesPath;

        var selectedFsm = await PickFsm(resourcesPath);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public async void FileOpenLast()
    {
        if (LastOpenedPath is null)
        {
            return;
        }

        if (!File.Exists(LastOpenedPath))
        {
            await MessageBoxUtil.ShowDialog($"No {LastOpenedPath}", $"Couldn't load {LastOpenedPath}. Was it deleted?");
            return;
        }

        var selectedFsm = await PickFsm(LastOpenedPath);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public async void ConfigSetGamePath()
    {
        // we're checking ggm path for sanity here
        var ggmPath = await PickGamePathWithFile("globalgamemanagers");
        if (ggmPath is null)
            return;

        ConfigurationManager.Settings.DefaultGamePath = Path.GetDirectoryName(ggmPath);
    }

    public void CloseTab()
    {
        if (ActiveDocument is not null)
            Documents.Remove(ActiveDocument);
    }

    public void CloseAllTabs()
    {
        ActiveDocument = null;
        Documents.Clear();
    }

    private static async Task<string?> PickGamePathWithFile(string fileName)
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
            return null;

        var result = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open a [Game name]_Data folder"
        });

        var folderNames = FileDialogUtils.GetOpenFolderDialogFolders(result);
        if (folderNames.Length == 0)
            return null;

        var folderName = folderNames[0];
        var filePath = Path.Combine(folderName, fileName);
        if (!File.Exists(filePath))
        {
            await MessageBoxUtil.ShowDialog($"No {fileName}", $"Couldn't load {fileName}. Did you open the right folder?");
            return null;
        }

        return filePath;
    }
}
