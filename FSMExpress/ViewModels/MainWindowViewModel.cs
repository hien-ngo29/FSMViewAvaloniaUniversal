using AssetsTools.NET.Extra;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using FSMExpress.Common.Assets;
using FSMExpress.Common.Document;
using FSMExpress.Logic.Util;
using FSMExpress.PlayMaker;
using FSMExpress.Services;
using FSMExpress.ViewModels.Dialogs;
using System.IO;
using System.Threading.Tasks;

namespace FSMExpress.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly AssetsManager _manager = new();

    [ObservableProperty]
    private FsmDocument? _activeDocument = null;

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
        var selectedFsm = await PickFsm(fileName);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public async void FileOpenSceneList()
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
            return;

        var result = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open a [Game name]_Data folder"
        });

        var folderNames = FileDialogUtils.GetOpenFolderDialogFolders(result);
        if (folderNames.Length == 0)
            return;

        var folderName = folderNames[0];
        var fileName = Path.Combine(folderName, "globalgamemanagers");
        if (!File.Exists(fileName))
        {
            await MessageBoxUtil.ShowDialog("No globalgamemanagers", "Couldn't load globalgamemanagers. Did you open the right folder?");
            return;
        }

        var scenePath = await PickScene(fileName);
        if (string.IsNullOrEmpty(scenePath))
            return;

        var selectedFsm = await PickFsm(scenePath);
        if (!selectedFsm.HasValue)
            return;

        LoadPlaymakerFsm(selectedFsm.Value);
    }

    public void FileOpenFsmJson()
    {

    }

    public void FileOpenResourcesAssets()
    {

    }

    public void FileOpenLast()
    {

    }

    public void ConfigSetGamePath()
    {

    }

    public void CloseTabs()
    {

    }

    public void CloseAllTabs()
    {

    }
}
