using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmBool : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public bool Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Boolean;

    public FsmBool()
    {
        Value = false;
    }

    public FsmBool(IAssetField field) : base(field)
    {
        Value = field.GetValue<bool>("value");
    }

    public override string ToString()
    {
        return Value.ToString().ToLowerInvariant();
    }
}