using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmGameObject : NamedVariable
{
    public NamedAssetPPtr Value { get; set; }

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
