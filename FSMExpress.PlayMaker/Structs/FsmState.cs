using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmState
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte ColorIndex { get; set; }
    public Rect Position { get; set; }
    public bool IsBreakpoint { get; set; }
    public bool IsSequence { get; set; }
    public bool HideUnused { get; set; }
    public List<FsmTransition> Transitions { get; set; }
    public FsmActionData ActionData { get; set; }

    public FsmState()
    {
        Name = string.Empty;
        Description = string.Empty;
        ColorIndex = 0;
        Position = new Rect();
        IsBreakpoint = false;
        IsSequence = false;
        HideUnused = false;
        Transitions = [];
        ActionData = new FsmActionData();
    }

    public FsmState(IAssetField field)
    {
        Name = field.GetValue<string>("name");
        Description = field.GetValue<string>("description");
        ColorIndex = field.GetValue<byte>("colorIndex");
        Position = new Rect(field.GetField("position"));
        IsBreakpoint = field.GetValue<bool>("isBreakpoint");
        IsSequence = field.GetValue<bool>("isSequence");
        HideUnused = field.GetValue<bool>("hideUnused");
        Transitions = field.GetValueArray("transitions", x => new FsmTransition(x));
        ActionData = new FsmActionData(field.GetField("actionData"));
    }
}
