using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmString : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public string Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.String;

    public FsmString()
    {
        Value = string.Empty;
    }

    public FsmString(IAssetField field) : base(field)
    {
        Value = field.GetValue<string>("value");
    }

    public override string ToString()
    {
        return Value;
    }
}