using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmTransition
{
    public FsmEvent Event { get; set; }
    public string ToState { get; set; }
    public int LinkStyle { get; set; }
    public int LinkConstraint { get; set; }
    public byte ColorIndex { get; set; }

    public FsmTransition()
    {
        Event = new FsmEvent();
        ToState = string.Empty;
        LinkStyle = 0;
        LinkConstraint = 0;
        ColorIndex = 0;
    }

    public FsmTransition(IAssetField field)
    {
        Event = new FsmEvent(field.GetField("fsmEvent"));
        ToState = field.GetValue<string>("toState");
        LinkStyle = field.GetValue<int>("linkStyle");
        LinkConstraint = field.GetValue<int>("linkConstraint");
        ColorIndex = field.GetValue<byte>("colorIndex");
    }
}
