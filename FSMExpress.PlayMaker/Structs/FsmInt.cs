using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmInt : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public int Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Integer;

    public FsmInt()
    {
        Value = 0;
    }

    public FsmInt(IAssetField field) : base(field)
    {
        Value = field.GetValue<int>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}