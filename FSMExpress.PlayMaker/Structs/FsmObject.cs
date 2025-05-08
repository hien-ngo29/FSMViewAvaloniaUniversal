using FSMExpress.Common.Assets;
using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmObject : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public NamedAssetPPtr Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmObject()
    {
        Value = new NamedAssetPPtr();
    }

    public FsmObject(IAssetField field) : base(field)
    {
        Value = field.GetValue<NamedAssetPPtr>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}