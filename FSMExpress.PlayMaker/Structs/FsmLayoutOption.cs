using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmLayoutOption
{
    public LayoutOptionType Option;
    public FsmFloat? FloatParam;
    public FsmBool? BoolParam;

    public FsmLayoutOption()
    {
        Option = LayoutOptionType.Width;
        FloatParam = new FsmFloat();
    }

    public FsmLayoutOption(IAssetField field)
    {
        Option = field.GetValue<LayoutOptionType>("option");
        switch (Option)
        {
            case LayoutOptionType.Width:
            case LayoutOptionType.Height:
            case LayoutOptionType.MinWidth:
            case LayoutOptionType.MaxWidth:
            case LayoutOptionType.MinHeight:
            case LayoutOptionType.MaxHeight:
                FloatParam = new FsmFloat(field.GetField("floatParam"));
                break;
            case LayoutOptionType.ExpandWidth:
            case LayoutOptionType.ExpandHeight:
                BoolParam = new FsmBool(field.GetField("boolParam"));
                break;
        }
    }
}

public enum LayoutOptionType
{
    Width,
    Height,
    MinWidth,
    MaxWidth,
    MinHeight,
    MaxHeight,
    ExpandWidth,
    ExpandHeight
}
