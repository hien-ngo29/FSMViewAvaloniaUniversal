using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class NamedVariable
{
    public bool UseVariable { get; set; }
    public string Name { get; set; }
    public string Tooltip { get; set; }
    public bool ShowInInspector { get; set; }
    public bool NetworkSync { get; set; }

    public NamedVariable()
    {
        UseVariable = true;
        Name = string.Empty;
        Tooltip = string.Empty;
        ShowInInspector = false;
        NetworkSync = false;
    }

    public NamedVariable(IAssetField field)
    {
        UseVariable = field.GetValue<bool>("useVariable");
        Name = field.GetValue<string>("name");
        Tooltip = field.GetValue<string>("tooltip");
        ShowInInspector = field.GetValue<bool>("showInInspector");
        NetworkSync = field.GetValue<bool>("networkSync");
    }
}
