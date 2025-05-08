using FSMExpress.Common.Assets;
using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmGameObject : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public NamedAssetPPtr Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmGameObject()
    {
        Value = new NamedAssetPPtr();
    }

    public FsmGameObject(IAssetField field) : base(field)
    {
        Value = field.GetValue<NamedAssetPPtr>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
