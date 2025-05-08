using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmLayoutOption : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public LayoutOptionType Option { get; set; }
    public FsmFloat? FloatParam { get; set; }
    public FsmBool? BoolParam { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmLayoutOption()
    {
        Option = LayoutOptionType.Width;
        FloatParam = new FsmFloat();
    }

    public FsmLayoutOption(IAssetField field) : base(field)
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
