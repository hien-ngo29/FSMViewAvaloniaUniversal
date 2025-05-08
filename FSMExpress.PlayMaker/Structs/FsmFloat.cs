using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmFloat : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public float Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Float;

    public FsmFloat()
    {
        Value = 0;
    }

    public FsmFloat(IAssetField field) : base(field)
    {
        Value = field.GetValue<float>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}