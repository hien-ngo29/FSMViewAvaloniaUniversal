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
    }

    public async void FileOpen()
    {
        var storageProvider = StorageService.GetStorageProvider();
        var dialogService = Ioc.Default.GetRequiredService<IDialogService>();
        if (storageProvider is null || dialogService is null)
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
        var fileInst = _manager.LoadAssetsFile(fileName);
        if (!_manager.LoadClassDatabase(fileInst))
        {
            await MessageBoxUtil.ShowDialog("Class Database failed to load", "Couldn't load class database class. Check if classdata.tpk exists?");
            return;
        }

        var fsmChoice = await dialogService.ShowDialog(new FsmSelectorViewModel(_manager, fileInst));
        if (fsmChoice == null)
            return;

        var fsmFileInst = _manager.FileLookup[fsmChoice.Ptr.FilePath];
        var fsmBaseField = _manager.GetBaseField(fsmFileInst, fsmChoice.Ptr.PathId);
        var fsmObject = new FsmPlaymaker(new AfAssetField(fsmBaseField["fsm"], new AfAssetNamer(_manager, fsmFileInst)));
        var fsmDoc = fsmObject.MakeDocument();
        ActiveDocument = fsmDoc;
    }

    public void FileOpenSceneList()
    {

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
