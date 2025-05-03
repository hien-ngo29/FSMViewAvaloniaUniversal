using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmEvent
{
    public string Name { get; set; }
    public bool IsSystemEvent { get; set; }
    public bool IsGlobal { get; set; }

    public FsmEvent()
    {
        Name = string.Empty;
        IsSystemEvent = false;
        IsGlobal = false;
    }

    public FsmEvent(IAssetField field)
    {
        Name = field.GetValue<string>("name");
        IsSystemEvent = field.GetValue<bool>("isSystemEvent");
        IsGlobal = field.GetValue<bool>("isGlobal");
    }
}
